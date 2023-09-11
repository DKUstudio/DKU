using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DKU_Server
{
    // 단순 accept 처리만을 위한 클래스
    public class CListener
    {
        // 비동기 accept를 위한 args
        SocketAsyncEventArgs _args;

        // 클라이언트의 접속을 처리할 소켓
        Socket _socket;

        // Accept 중 block하기 위한 이벤트 변수
        AutoResetEvent _flow_event;

        // 새로운 클라이언트가 접속했을 때 호출되는 콜백
        public Action<Socket, object> _onNewClient_callback;

        public CListener()
        {
            _onNewClient_callback = null;
        }

        // 초기화
        public void Start(int backlog)
        {
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            // 소켓 생성
            _socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                _socket.Bind(endPoint);
                _socket.Listen(backlog);

                _args = new SocketAsyncEventArgs();
                _args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);   // 이벤트 최초 등록

                Task listen_task = new Task(ListenTask);
                listen_task.Start();     // 무한반복 비동기 태스크 시작
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void ListenTask()
        {
            _flow_event = new AutoResetEvent(false);

            while (true)
            {
                // 재사용하기 위해 null로 둠, flow_event로 관리하기 때문에 보장됨
                _args.AcceptSocket = null;

                bool pending = true;
                try
                {
                    pending = _socket.AcceptAsync(_args);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                // 즉시 완료되면 이벤트가 실행이 안됨, 그래서 수동 처리
                if (pending == false)
                {
                    OnAcceptCompleted(null, _args);
                }

                _flow_event.WaitOne();    // 잠시 대기
            }
        }

        void OnAcceptCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                // 새로 생긴 소켓 받아옴
                Socket clientSocket = args.AcceptSocket;

                _flow_event.Set(); // 다음 연결 빠르게 ㄱㄱ

                // accept까지만 수행하고 클라이언트 연결 후 처리는 외부로 넘김

                if (_onNewClient_callback != null)
                {
                    _onNewClient_callback.Invoke(clientSocket, args.UserToken);
                }

                return;
            }
            else
            {
                Console.WriteLine("Failed to accept client");
            }

            _flow_event.Set(); // 다음 연결 ㄱㄱ
        }
    }
}
