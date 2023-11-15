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
using Org.BouncyCastle.Ocsp;
using DKU_Server.Worlds;

namespace DKU_Server.Connections.Tokens
{
    public class UserToken
    {
        public Socket m_socket { get; set; } // 연결된 소켓
        public UserData? udata;
        public LoginData ldata = new LoginData();

        SMessageResolver m_message_resolver; // 받은 데이터를 Packet 객체로 만들어줌
        // 요청받은 패킷 리스트
        List<SPacket> m_recv_packet_list;
        // 보낼 패킷 리스트
        Queue<Packet> m_send_packet_queue;

        SocketAsyncEventArgs m_recv_args;   // 메시지 받을 때 사용
        SocketAsyncEventArgs m_send_args;   // 메시지 보낼 때 사용

        DateTime m_last_connection;

        public UserToken()
        {
            m_message_resolver = new SMessageResolver();
            m_recv_packet_list = new List<SPacket>(10);
            m_send_packet_queue = new Queue<Packet>(100);
            m_recv_args = SocketAsyncEventArgsPool.Instance.Pop();
            m_send_args = SocketAsyncEventArgsPool.Instance.Pop();

            // force timeout check
            m_last_connection = DateTime.Now;
            Task t = new Task(CheckConnectionTimeout);
            t.Start();
        }

        public UserToken(bool timeout_check)
        {
            m_message_resolver = new SMessageResolver();
            m_recv_packet_list = new List<SPacket>(10);
            m_send_packet_queue = new Queue<Packet>(100);
            m_recv_args = SocketAsyncEventArgsPool.Instance.Pop();
            m_send_args = SocketAsyncEventArgsPool.Instance.Pop();

            // timeout check
            if (timeout_check)
            {
                m_last_connection = DateTime.Now;
                Task t = new Task(CheckConnectionTimeout);
                t.Start();
            }
            else
            {
                // false인 경우 로그인 서버 접속이므로 강제 수동 할당 해줌
                // 수신용 객체 설정
                m_recv_args.Completed += onRecvCompleted;
                m_recv_args.UserToken = this;

                // 송신용 객체 설정
                m_send_args.Completed += onSendCompleted;
                m_send_args.UserToken = this;

                m_recv_args.SetBuffer(new byte[CommonDefine.SOCKET_BUFFER_SIZE], 0, CommonDefine.SOCKET_BUFFER_SIZE);
                m_send_args.SetBuffer(new byte[CommonDefine.SOCKET_BUFFER_SIZE], 0, CommonDefine.SOCKET_BUFFER_SIZE);
            }
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

        #region receive
        public void StartRecv()
        {
            try
            {
                if (m_socket == null || m_socket.Connected == false)
                {
                    return;
                }
                bool pending = m_socket.ReceiveAsync(m_recv_args);
                if (!pending)
                    onRecvCompleted(null, m_recv_args);
            }
            catch (Exception e)
            {
                LogManager.Log(e.ToString());
            }
        }

        void onRecvCompleted(object? sender, SocketAsyncEventArgs args)
        {
            try
            {
                if (m_socket != null && m_socket.Connected == true && args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
                {
                    // read message
                    //LogManager.Log("[usertoken] recv completed");
                    m_message_resolver.onRecv(args.Buffer, args.Offset, args.BytesTransferred, onMessageCompleted);
                    m_last_connection = DateTime.Now;

                    StartRecv();
                }
                else
                {
                    // COMPLETE_MESSAGE_SIZE_CLIENT
                }
            }
            catch (Exception e)
            {
                LogManager.Log(e.ToString());
            }
        }

        void onMessageCompleted(SPacket packet)
        {
            if (packet == null)
                return;
            try
            {
                if (m_socket == null || m_socket.Connected == false)
                    return;
                packet.SetUserToken(this);
                NetworkManager.Instance.m_game_packet_handler.ParsePacket(packet);
            }
            catch (Exception e)
            {
                LogManager.Log(e.ToString());
            }
        }
        #endregion

        #region send
        public void Send(Packet packet)
        {
            try
            {
                if (m_socket == null || m_socket.Connected == false)
                    return;

                //lock (m_send_packet_queue)
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
                    LogManager.Log($"[Send] packet counts {m_send_packet_queue.Count}");
                }
            }
            catch (Exception e)
            {
                if (m_send_packet_queue == null)
                {
                    LogManager.Log("[Closed] null send queue");
                    return;
                }
                LogManager.Log(e.ToString());
            }
        }

        void SendProcess()
        {
            try
            {
                if (m_socket == null || m_socket.Connected == false)
                {
                    LogManager.Log("[Socket] m_socket is null");
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
            catch (Exception e)
            {
                LogManager.Log(e.ToString());
            }
        }

        void onSendCompleted(object? sender, SocketAsyncEventArgs args)
        {
            try
            {
                if (m_socket != null && m_socket.Connected == true && args.SocketError == SocketError.Success)
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
            catch (Exception e)
            {
                LogManager.Log(e.ToString());
            }
        }

        void onSendCompletedPooling(object sender, SocketAsyncEventArgs args)
        {
            try
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
            catch (Exception e)
            {
                LogManager.Log(e.ToString());
            }
        }
        #endregion

        public void Close()
        {
            try
            {
                if (m_recv_packet_list != null)
                {
                    m_recv_packet_list.Clear();
                    m_recv_packet_list = null;
                }
                if (m_send_packet_queue != null)
                { 
                    m_send_packet_queue.Clear();
                    m_send_packet_queue = null;
                }
                if (udata != null)
                {
                    NetworkManager.Instance.world.RemoveUidUser(udata.uid);
                    LogManager.Log($"[UserToken] goodbye, {udata.nickname}");
                }

                if (m_socket != null)
                {
                    LogManager.Log($"[UserToken] goodbye, {m_socket.RemoteEndPoint.ToString()}");
                    m_socket.Shutdown(SocketShutdown.Both);
                    m_socket.Close();
                    m_socket = null;
                }

                SocketAsyncEventArgs e_args = m_recv_args;
                m_recv_args = null;
                BufferManager.Instance.FreeBuffer(e_args);
                e_args = m_send_args;
                m_send_args = null;
                BufferManager.Instance.FreeBuffer(e_args);

            }
            catch (Exception e)
            {
                LogManager.Log(e.ToString());
            }
        }

        void CheckConnectionTimeout()
        {
            while (true)
            {
                Thread.Sleep(5000);
                if ((int)DateTime.Now.Subtract(m_last_connection).TotalSeconds > CommonDefine.MAX_TOKEN_CONNECTION_TIMEOUT)   // 3분간 완복패킷 없으면 세션 종료
                {
                    LogManager.Log($"[Close Connection] connection timeout {m_socket.RemoteEndPoint.ToString()}");

                    break;
                }
            }
            Close();
        }
    }
}
