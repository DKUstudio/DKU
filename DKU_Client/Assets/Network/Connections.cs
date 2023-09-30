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

public class Connections : MonoBehaviour
{
    Socket m_socket;
    SocketAsyncEventArgs m_recv_args;
    LinkedList<Packet> m_recv_packet_list;
    MessageResolver m_message_resolver;
    GamePacketHandler m_game_packet_handler;

    // public Action<bool> on_connection_completed;

    private bool connected = false;
    public bool Connected => connected;

    public bool is_waiting = false;
    [Sirenix.OdinInspector.ReadOnly]
    public long waiting_id;
    public bool logged_in = false;
    [Sirenix.OdinInspector.ReadOnly]
    public UserData udata;

    private void Update()
    {
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
    // public void Connect(string address, int port)
    // {
    //     m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //     m_socket.NoDelay = true;

    //     IPAddress target = IPAddress.Parse(address);
    //     IPEndPoint endPoint = new IPEndPoint(target, port);

    //     // 접속용 args
    //     SocketAsyncEventArgs args = new SocketAsyncEventArgs();
    //     args.Completed += onConnected;
    //     args.RemoteEndPoint = endPoint;

    //     bool pending = m_socket.ConnectAsync(args);
    //     if (!pending)
    //         onConnected(null, args);
    // }
    public void Connect()
    {
        m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        m_socket.NoDelay = true;
        Debug.Log("[Connections] Try connect server...");

        IPAddress target = IPAddress.Parse(CommonDefine.IPv4_ADDRESS);
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
            Debug.Log("[Connections] Server <color=green>connected</color>");
            connected = true;
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
        bool pending = m_socket.ReceiveAsync(m_recv_args);
        if (!pending)
            onRecvCompleted(null, m_recv_args);
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
        PushPacket(packet);
    }

    void PushPacket(Packet packet)
    {
        m_recv_packet_list.AddLast(packet);
    }

    void ProcessPackets()
    {
        for (int i = 0; i < m_recv_packet_list.Count; i++)
            m_game_packet_handler.ParsePacket(m_recv_packet_list.ElementAt(i));
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

    public void Send_Async(Packet packet)
    {
        if (m_socket == null || m_socket.Connected == false)
            return;
        SocketAsyncEventArgs send_args = SocketAsyncEventArgsPool.Instance.Pop();

        byte[] send_data = packet.GetSendBytes();
        send_args.SetBuffer(send_data, 0, send_data.Length);

        m_socket.Send(send_data);
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
        is_waiting = v_is_waiting;
        waiting_id = v_is_waiting ? v_waiting_id : -1;
    }

    public void SetLogin(bool v_logged_in, UserData v_udata)
    {
        logged_in = v_logged_in;
        udata = v_logged_in ? v_udata : null;
    }

    private void OnApplicationQuit()
    {
        // 자동 로그아웃 및 대기열 탈출
        if (logged_in == true)
        {
            C_LogoutReq req = new C_LogoutReq();
            req.uid = udata.uid;

            byte[] body = req.Serialize();
            Packet packet = new Packet(PacketType.C_LogoutReq, body, body.Length);
            Send_Async(packet);
        }

        if (is_waiting)
        {
            C_StopWaitingReq req = new C_StopWaitingReq();
            req.waiting_id = NetworkManager.Instance.Connections.waiting_id;
            byte[] body = req.Serialize();

            Packet packet = new Packet(PacketType.C_StopWaitingReq, body, body.Length);
            NetworkManager.Instance.Connections.Send(packet);
        }

        m_socket = null;
    }
}
