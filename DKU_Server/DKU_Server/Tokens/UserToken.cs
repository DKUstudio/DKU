using DKU_Server.Users;
using DKU_ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Tokens
{
    public class UserToken
    {
        public UserData User {  get; set; } // 유저 정보 저장

        MessageResolver m_message_resolver; // 받은 데이터를 Packet 객체로 만들어줌
        public Socket m_socket { get; set; } // 연결된 소켓

        // 요청받은 패킷 리스트
        List<Packet> m_packet_list = new List<Packet>(5);
        object m_mutex_packet_list = new object();
        SocketAsyncEventArgs m_recv_args;   // 메시지 받을 때 사용


        // 보낼 패킷 리스트
        Queue<Packet> m_sned_packet_queue = new Queue<Packet>(100);
        object m_mutex_send_list = new object();
        SocketAsyncEventArgs m_send_args;   // 메시지 보낼 때 사용

        public UserToken()
        {
            m_message_resolver = new MessageResolver();
        }

        public void Init()
        {
            // 수신용 객체 설정
            m_recv_args = SocketAsyncEventArgsPool.Instance.Pop();
            m_recv_args.Completed += onRecvCompleted;
            m_recv_args.UserToken = this;

            // 송신용 객체 설정
            m_send_args = SocketAsyncEventArgsPool.Instance.Pop();
            m_send_args.Completed += onSendCompleted;
            m_send_args.UserToken = this;

            // 송수신용 객체 모두 버퍼매니저를 이용해 세팅
            BufferManager.Instance.SetBuffer(m_recv_args);
            BufferManager.Instance.SetBuffer(m_send_args);
        }

        public void StartRecv()
        {
            bool pending = m_socket.ReceiveAsync(m_recv_args);
            if (!pending)
                onRecvCompleted(null, m_recv_args);
        }

        void onRecvCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
            {
                // read message

                StartRecv();
            }
            else
            {
                // disconect
            }
        }

        void onSendCompleted(object sender, SocketAsyncEventArgs args)
        {

        }
    }
}
