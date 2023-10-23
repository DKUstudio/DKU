using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using UnityEngine;
using Sirenix.OdinInspector;
using DKU_ServerCore.Packets.var.client;
using System.Threading;
using DKU_ServerCore.Packets.var.qclient;
using UnityEngine.SceneManagement;

public class Connections : MonoBehaviour
{
    Socket m_socket;
    public Socket M_socket => m_socket;
    SocketAsyncEventArgs m_recv_args;
    LinkedList<Packet> m_recv_packet_list;
    MessageResolver m_message_resolver;
    GamePacketHandler m_game_packet_handler;

    // public Action<bool> on_connection_completed;

    [Sirenix.OdinInspector.ReadOnly]
    [ShowInInspector]
    private bool connected = false;
    public bool Connected => connected;

    private void Update()
    {
        if (connected)
            ProcessPackets();
    }

    public void Init()
    {
        m_recv_args = new SocketAsyncEventArgs();
        m_recv_args.Completed += onRecvCompleted;
        m_recv_args.UserToken = this;
        m_recv_args.SetBuffer(new byte[CommonDefine.SOCKET_BUFFER_SIZE], 0, CommonDefine.SOCKET_BUFFER_SIZE);

        m_recv_packet_list = new LinkedList<Packet>();
        m_message_resolver = new MessageResolver();
        m_game_packet_handler = new GamePacketHandler();
    }

    #region connect
    public void Connect()
    {
        m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        m_socket.NoDelay = true;
        Debug.Log($"[Connections] Try connect server...{CommonDefine.LOGIN_QUEUE_IPv4_ADDRESS}");

        IPAddress target = IPAddress.Parse(CommonDefine.LOGIN_QUEUE_IPv4_ADDRESS);
        IPEndPoint endPoint = new IPEndPoint(target, CommonDefine.IP_PORT);

        // 접속용 args
        SocketAsyncEventArgs args = new SocketAsyncEventArgs();
        args.Completed += onConnected;
        args.RemoteEndPoint = endPoint;

        bool pending = m_socket.ConnectAsync(args);
        if (!pending)
            onConnected(null, args);
    }

    public void Connect(string ip_address)
    {
        m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        m_socket.NoDelay = true;

        Debug.Log($"[Connections] Try connect server...{ip_address}");
        IPAddress target = IPAddress.Parse(ip_address);
        IPEndPoint endPoint = new IPEndPoint(target, CommonDefine.IP_PORT);

        // 접속용 args
        SocketAsyncEventArgs args = new SocketAsyncEventArgs();
        args.Completed += onConnected;
        args.RemoteEndPoint = endPoint;

        bool pending = m_socket.ConnectAsync(args);
        if (!pending)
            onConnected(null, args);
    }

    void onConnected(object sender, SocketAsyncEventArgs args)
    {
        if (args.SocketError == SocketError.Success)
        {
            Debug.Log($"[Connections] Server <color=green>connected</color> {m_socket.RemoteEndPoint}");
            this.connected = true;
            if (NetworkManager.Instance.IS_LOGGED_IN)
            {
                C_MyUserDataReq req = new C_MyUserDataReq();
                req.uid = NetworkManager.Instance.UDATA.uid;
                req.udata = NetworkManager.Instance.UDATA;
                byte[] body = req.Serialize();

                Packet packet = new Packet(PacketType.C_MyUserDataReq, body, body.Length);
                Send(packet);

                try
                {
                    MainThreadDispatcher.mtd.AddJob(() => SceneManager.LoadScene("MainMap"));
                    //SceneManager.LoadScene("MainMap");
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }
                Debug.Log("여기서 인게임 씬 전환");
            }

            StartRecv();
            // on_connection_completed.Invoke(true);
        }
        else
        {
            // on_connection_completed.Invoke(false);
            Debug.Log("[Connections] Server connection <color=red>failed</color>... Retry connection");
            if (m_socket != null)
                Connect();
            else
                Debug.Log("[Connnections] m_socket is <color=red>null</color>...");
        }
    }
    #endregion

    #region recv
    void StartRecv()
    {
        try
        {
            if (m_socket.Connected == false)
            {
                Debug.Log("[GameServer] socket disconnected");
                return;
            }
            //Debug.Log($"start recv {m_socket.RemoteEndPoint}");
            bool pending = m_socket.ReceiveAsync(m_recv_args);
            if (!pending)
                onRecvCompleted(null, m_recv_args);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    void onRecvCompleted(object sender, SocketAsyncEventArgs args)
    {
        if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
        {
            m_message_resolver.onRecv(args.Buffer, args.Offset, args.BytesTransferred, onMessageCompleted);
        }
        StartRecv();
    }

    void onMessageCompleted(Packet packet)
    {
        //Debug.Log($"[Packet] push packet {(PacketType)packet.m_type}");
        PushPacket(packet);
    }

    void PushPacket(Packet packet)
    {
        m_recv_packet_list.AddLast(packet);
    }

    void ProcessPackets()
    {
        for (int i = 0; i < m_recv_packet_list.Count; i++)
        {
            //Debug.Log($"[Packet] process packet {(PacketType)m_recv_packet_list.ElementAt(0).m_type}");
            m_game_packet_handler.ParsePacket(m_recv_packet_list.ElementAt(i));
        }
        m_recv_packet_list.Clear();
    }
    #endregion

    #region send
    public void Send(Packet packet)
    {
        if (m_socket == null || m_socket.Connected == false)
            return;

        SocketAsyncEventArgs send_args = SocketAsyncEventArgsPool.Instance.Pop();

        byte[] send_data = packet.GetSendBytes();
        send_args.SetBuffer(send_data, 0, send_data.Length);

        bool pending = m_socket.SendAsync(send_args);
        if (!pending)
            onSendCompleted(null, send_args);
    }

    public void Send_Sync(Packet packet)
    {
        if (m_socket == null || m_socket.Connected == false)
            return;
        SocketAsyncEventArgs send_args = SocketAsyncEventArgsPool.Instance.Pop();

        byte[] send_data = packet.GetSendBytes();
        send_args.SetBuffer(send_data, 0, send_data.Length);

        m_socket.Send(send_data);
        //Debug.Log("[Send_sync] complete");
    }
    void onSendCompleted(object sender, SocketAsyncEventArgs args)
    {
        if (args.SocketError == SocketError.Success)
        {

        }
        else
        {

        }
        args.Completed -= onSendCompleted;
        SocketAsyncEventArgsPool.Instance.Push(args);
    }
    #endregion

    public void SetWaiting(bool v_is_waiting, long v_waiting_id)
    {
        NetworkManager.Instance.SetIsWaiting(v_is_waiting);
        NetworkManager.Instance.SetWid(v_is_waiting ? v_waiting_id : 0);
    }

    public void SetLogin(bool v_logged_in, UserData v_udata)
    {
        NetworkManager.Instance.SetIsLoggedIn(v_logged_in);
        NetworkManager.Instance.SetUdata(v_logged_in ? v_udata : null);
    }

    public void CloseSocketConnection()
    {
        m_socket.Close();
        m_socket = null;

        m_recv_args = null;
        m_recv_args = new SocketAsyncEventArgs();
    }

    private void OnApplicationQuit()
    {
        //Debug.Log("OnAppQuit");
        // 자동 대기열 탈출
        if (NetworkManager.Instance.IS_WAITING == true)
        {
            QC_LogoutReq req = new QC_LogoutReq();
            req.wid = NetworkManager.Instance.WID;
            byte[] body = req.Serialize();

            Packet packet = new Packet(PacketType.QC_LogoutReq, body, body.Length);
            Send_Sync(packet);
        }

        // 자동 로그아웃
        if (NetworkManager.Instance.IS_LOGGED_IN == true)
        {
            //Debug.Log("[Logout] GS");
            C_LogoutReq req = new C_LogoutReq();
            req.uid = NetworkManager.Instance.UDATA.uid;
            if (NetworkManager.Instance.UDATA != null)
                req.uid = NetworkManager.Instance.UDATA.uid;
            else
                req.uid = -1;

            byte[] body = req.Serialize();

            Packet packet = new Packet(PacketType.C_LogoutReq, body, body.Length);
            Send_Sync(packet);
            //Debug.Log("[Logout] send complete");
        }

        m_socket = null;
    }
}
