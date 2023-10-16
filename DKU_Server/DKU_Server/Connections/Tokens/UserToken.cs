using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_Server.Utils;

namespace DKU_Server.Connections.Tokens
{
    public class UserToken
    {
        SMessageResolver m_message_resolver; // 받은 데이터를 Packet 객체로 만들어줌
        public Socket? m_socket { get; set; } // 연결된 소켓

        // 요청받은 패킷 리스트
        List<SPacket> m_recv_packet_list;
        // 보낼 패킷 리스트
        Queue<Packet> m_send_packet_queue;

        SocketAsyncEventArgs m_recv_args;   // 메시지 받을 때 사용
        SocketAsyncEventArgs m_send_args;   // 메시지 보낼 때 사용

        public UserToken()
        {
            m_message_resolver = new SMessageResolver();
            m_recv_packet_list = new List<SPacket>(10);
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
            BufferManager.Instance.SetBuffer(m_recv_args);
            BufferManager.Instance.SetBuffer(m_send_args);
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
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }

        #region receive
        public void StartRecv()
        {
            if(m_socket == null)
            {
                Console.WriteLine("m_socket is null");
                return;
            }
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

        void onMessageCompleted(SPacket packet)
        {
            /*string str = Encoding.Unicode.GetString(packet.m_data);
            Console.WriteLine(str);*/
            //Console.WriteLine((PacketType)packet.m_type);
            packet.SetUserToken(this);
            NetworkManager.Instance.m_game_packet_handler.ParsePacket(packet);
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
                Console.WriteLine($"[Send] packet counts {m_send_packet_queue.Count}");
            }

        }

        void SendProcess()
        {
            if (m_socket == null)
            {
                Console.WriteLine("[Socket] m_socket is null");
                return;
            }

            Packet packet = m_send_packet_queue.Peek();
            byte[] send_data = packet.GetSendBytes();

            int data_len = send_data.Length;

            if (data_len > CommonDefine.SOCKET_BUFFER_SIZE)
            {
                // 버퍼 풀에서 설정한 크기가 충분하지 않은 경우 (4K 초과)
                // SocketAsyncEventArgsPool에서 새 객체를 Pop해서 사용
                // 점속 초기에 큰 데이터 큰 것을 보낼때만 상용됨
                // 큰 데이터를 보낼 일이 없으면 무시 가능
                SocketAsyncEventArgs send_event_args = SocketAsyncEventArgsPool.Instance.Pop();
                if (send_event_args == null)
                {
                    Console.WriteLine("SocketAsyncEventArgsPool::Pop() result is null");
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
            Console.WriteLine($"send complete");
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

        void onSendCompletedPooling(object sender, SocketAsyncEventArgs args)
        {
            if (args.BufferList != null)
                args.BufferList = null;
            args.SetBuffer(null, 0, 0);
            args.UserToken = null;
            args.RemoteEndPoint = null;
            args.Completed -= onSendCompletedPooling;
            SocketAsyncEventArgsPool.Instance.Push(args);

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

        void AddPacket(SPacket packet)
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
