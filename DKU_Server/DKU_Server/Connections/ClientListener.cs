using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DKU_Server.Connections
{
    public class ClientListener
    {
        SocketAsyncEventArgs m_accept_args;
        Socket m_listen_socket;
        AutoResetEvent m_flow_control_event;
        bool m_thread_live { get; set; }

        public Action<Socket, object> m_callback_on_new_client;

        public ClientListener()
        {
            m_callback_on_new_client = null;
            m_thread_live = true;
        }

        public void Start(string host, int port, int backlog)
        {
            // 소켓을 연다
            m_listen_socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
            m_listen_socket.NoDelay = true;

            IPAddress address;
            if (host == "0.0.0.0")
            {
                address = IPAddress.Any;
            }
            else
            {
                address = IPAddress.Parse(host);
            }
            IPEndPoint endPoint = new IPEndPoint(address, port);
            Console.WriteLine(endPoint);

            try
            {
                // 서버 ip에 소켓을 연결하고, 수신상태로 만든다.
                m_listen_socket.Bind(endPoint);
                m_listen_socket.Listen(backlog);

                // 유저가 연결되었을 떄, 발생할 이벤트를 등록한다.
                m_accept_args = new SocketAsyncEventArgs();
                m_accept_args.Completed += new EventHandler<SocketAsyncEventArgs>(onAcceptCompleted);

                // 별도의 스레드에서
                Thread listen_thread = new Thread(doListen);
                listen_thread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void doListen()
        {
            Console.WriteLine("listen ready...");
            // 하나의 유저를 받고 처리를 기다리도록 하기 위해 사용
            m_flow_control_event = new AutoResetEvent(false);

            // 서버가 살아있는 동안, 유저를 기다림
            while (m_thread_live)
            {
                // 초기화 부분
                m_accept_args.AcceptSocket = null;
                bool pending = false;

                try
                {
                    // 비동기 함수로 accept 함
                    pending = m_listen_socket.AcceptAsync(m_accept_args);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                // 바로 accept 된 경우
                if (pending == false)
                {
                    onAcceptCompleted(null, m_accept_args);
                }

                // 스레드를 대기시킴, m_flow_control_event.Set()이 발생하면 재개
                m_flow_control_event.WaitOne();
            }
        }

        void onAcceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {

                // 새로운 유저가 접속 했을 때,
                Socket client_socket = e.AcceptSocket;

                // 접속 처리 부분
                NetworkManager.Instance.onNewClient(client_socket, e);
            }
            else
            {
                // 실패
                Console.WriteLine("[server] failed new client");
            }

            // 위의 스레드를 재개시켜, 다음 유저의 접속을 기다린다.
            m_flow_control_event.Set();
        }

        public void Close()
        {
            m_listen_socket.Close();
        }
    }
}
