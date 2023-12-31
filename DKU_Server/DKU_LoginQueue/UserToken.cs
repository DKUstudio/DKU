﻿using DKU_ServerCore;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DKU_LoginQueue
{
    public class UserToken
    {
        MessageResolver? m_message_resolver; // 받은 데이터를 Packet 객체로 만들어줌
        public Socket? m_socket { get; set; } // 연결된 소켓

        // 요청받은 패킷 리스트
        List<Packet>? m_recv_packet_list;
        // 보낼 패킷 리스트
        Queue<Packet>? m_send_packet_queue;

        SocketAsyncEventArgs? m_recv_args;   // 메시지 받을 때 사용
        SocketAsyncEventArgs? m_send_args;   // 메시지 보낼 때 사용

        public UserToken()
        {
            m_message_resolver = new MessageResolver();
            m_recv_packet_list = new List<Packet>(10);
            m_send_packet_queue = new Queue<Packet>(100);
            m_recv_args = SocketAsyncEventArgsPool.Instance.Pop();
            m_send_args = SocketAsyncEventArgsPool.Instance.Pop();
        }

        public void Init()
        {
            // 수신용 객체 설정
            m_recv_args.Completed += onRecvCompleted;
            m_recv_args.UserToken = this;

            // 송신용 객체 설정
            m_send_args.Completed += onSendCompleted;
            m_send_args.UserToken = this;

            // 송수신용 객체 모두 버퍼매니저를 이용해 세팅
            byte[] recv_buffer = new byte[128];
            byte[] send_buffer = new byte[128];
            m_recv_args.SetBuffer(recv_buffer, 0, recv_buffer.Length);
            m_send_args.SetBuffer(send_buffer, 0, send_buffer.Length);
        }

        public void Update()
        {
            if (m_recv_packet_list.Count > 0)
            {
                lock (m_recv_packet_list)
                {
                    try
                    {
                        // 수신 패킷 처리
                        /*foreach (Packet packet in m_packet_list)
                            user.ProcessPacket(packet);*/
                        m_recv_packet_list.Clear();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }

        #region receive
        public void StartRecv()
        {
            bool pending = m_socket.ReceiveAsync(m_recv_args);
            if (!pending)
                onRecvCompleted(null, m_recv_args);
        }

        void onRecvCompleted(object? sender, SocketAsyncEventArgs args)
        {
            if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
            {
                // read message
                m_message_resolver.onRecv(args.Buffer, args.Offset, args.BytesTransferred, onMessageCompleted);

                StartRecv();
            }
            else
            {
                // COMPLETE_MESSAGE_SIZE_CLIENT
            }
        }

        void onMessageCompleted(Packet packet)
        {
            if (packet == null)
                return;
            try
            {
                if (m_socket == null || m_socket.Connected == false)
                    return;
                NetworkManager.Instance.m_game_packet_handler.ParsePacket(packet);
            }
            catch (Exception e)
            {
                LogManager.Log(e.ToString());
            }
            //NetworkManager.Instance.m_game_packet_handler.ParsePacket(packet);
        }
        #endregion

        #region send
        public void Send(Packet packet)
        {
            if (m_socket == null)
                return;

            lock (m_send_packet_queue)
            {
                // 수신 중인 패킷이 없으면, 바로 전송
                if (m_send_packet_queue.Count < 1)
                {
                    m_send_packet_queue.Enqueue(packet);
                    SendProcess();
                    return;
                }

                // 수신 중인 패킷이 있으면, 큐에 넣고 나감
                // 쌓인 패킷이 100개가 넘으면 그 다음부터는 무시함, 게임마다 케바케임..
                if (m_send_packet_queue.Count < 100)
                {
                    m_send_packet_queue.Enqueue(packet);
                }
            }

        }

        void SendProcess()
        {
            if (m_socket == null)
                return;

            Packet packet = m_send_packet_queue.Peek();
            byte[] send_data = packet.GetSendBytes();

            int data_len = send_data.Length;

            if (data_len > 128)
            {
                // 버퍼 풀에서 설정한 크기가 충분하지 않은 경우 (4K 초과)
                // SocketAsyncEventArgsPool에서 새 객체를 Pop해서 사용
                // 점속 초기에 큰 데이터 큰 것을 보낼때만 상용됨
                // 큰 데이터를 보낼 일이 없으면 무시 가능
                SocketAsyncEventArgs send_event_args = SocketAsyncEventArgsPool.Instance.Pop();
                if (send_event_args == null)
                {
                    LogManager.Log("SocketAsyncEventArgsPool::Pop() result is null");
                    return;
                }
                send_event_args.Completed += onSendCompletedPooling;
                send_event_args.UserToken = this;
                send_event_args.SetBuffer(send_data, 0, send_data.Length);

                bool pending = m_socket.SendAsync(send_event_args);
                if (pending == false)
                {
                    onSendCompletedPooling(null, send_event_args);
                }
            }
            else
            {
                // 버퍼풀에서 설정한 크기보다 작은 경우 (4K 이하)
                // 버퍼를 설정
                m_send_args.SetBuffer(m_send_args.Offset, send_data.Length);
                // 버퍼에 데이터를 복사
                Array.Copy(send_data, 0, m_send_args.Buffer, m_send_args.Offset, send_data.Length);

                bool pending = m_socket.SendAsync(m_send_args);
                if (pending == false)
                    onSendCompleted(null, m_send_args);
            }
        }

        void onSendCompleted(object? sender, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                lock (m_send_packet_queue)
                {
                    if (m_send_packet_queue.Count > 0)
                    {
                        m_send_packet_queue.Dequeue();
                    }
                    if (m_send_packet_queue.Count > 0)
                    {
                        SendProcess();
                    }
                }
            }
        }

        void onSendCompletedPooling(object? sender, SocketAsyncEventArgs args)
        {
            if (args.BufferList != null)
                args.BufferList = null;
            args.SetBuffer(null, 0, 0);
            args.UserToken = null;
            args.RemoteEndPoint = null;
            args.Completed -= onSendCompletedPooling;
            //SocketAsyncEventArgsPool.Instance.Push(args);

            if (args.SocketError == SocketError.Success)
            {
                lock (m_send_packet_queue)
                {
                    if (m_send_packet_queue.Count > 0)
                        m_send_packet_queue.Dequeue();

                    if (m_send_packet_queue.Count > 0)
                        SendProcess();
                }
            }
        }
        #endregion

        void AddPacket(Packet packet)
        {
            lock (m_recv_packet_list)
            {
                m_recv_packet_list.Add(packet);
            }
        }

        public void Close()
        {
            m_message_resolver = null;
            m_recv_packet_list = null;
            m_send_packet_queue = null;
            m_socket = null;

            SocketAsyncEventArgsPool.Instance.Push(m_recv_args);
            SocketAsyncEventArgsPool.Instance.Push(m_send_args);
        }
    }
}
