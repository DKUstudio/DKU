using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using UnityEngine;

public class Connections : MonoBehaviour
{
    Socket m_socket;
    SocketAsyncEventArgs m_recv_args;
    LinkedList<Packet> m_recv_packet_list;
    MessageResolver m_message_resolver;
    GamePacketHandler m_game_packet_handler;

    Action<bool> on_connection_completed;

    private void Update()
    {
        ProcessPackets();
    }

    public void Init(Action<bool> complete_action)
    {
        on_connection_completed = complete_action;

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
            StartRecv();
            on_connection_completed.Invoke(true);
        }
        else
        {
            on_connection_completed.Invoke(false);
            Connect();
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
}
