using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.UserToken
{
    class UserToken
    {
        SocketAsyncEventArgs m_recv_args;
        MessageResolver m_message_resolver;
        Socket m_socket;

        // 요청받은 패킷 리스트


        SocketAsyncEventArgs m_send_args;

        // 보낼 패킷 리스트


        public UserToken()
        {
            m_message_resolver = new MessageResolver();
        }

        public void Init()
        {
            // 수신용 객체 설정
            

            // 송신용 객체 설정

            // 송수신용 객체 모두 버퍼매니저를 이용해 세팅
        }

        public void StartRecv()
        {
            bool pending = m_socket.ReceiveAsync(m_recv_args);
            if (!pending)
                OnRecvCompleted(null, m_recv_args);
        }

        void OnRecvCompleted(object sender, SocketAsyncEventArgs args)
        {
            if(args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
            {
                // read message

                StartRecv();
            }
            else
            {
                // disconect
            }
        }
    }
}
