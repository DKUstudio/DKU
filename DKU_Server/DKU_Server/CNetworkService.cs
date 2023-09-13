using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DKU_ServerCore;

namespace DKU_Server
{
    class CNetworkService
    {
        const int _maxConnections = 10;
        CListener _listener;    // 클라이언트 수신

        SocketAsyncEventArgsPool _recvArgs_pool;
        SocketAsyncEventArgsPool _sendArgs_pool;

        BufferManager _bufferManager;

        public Action<CUserToken> _onSessionCreated_callback;

        public CNetworkService()
        {
            _recvArgs_pool = new SocketAsyncEventArgsPool(_maxConnections);
            _sendArgs_pool = new SocketAsyncEventArgsPool(_maxConnections);
        }

        // Accept하는 태스크 실행
        public void Listen(int backlog)
        {
            _listener = new CListener();
            _listener._onNewClient_callback += OnNewClient;
            _listener.Start(backlog);
        }

        void OnNewClient(Socket clientSocket, object o)
        {
            // 풀에서 꺼내와 사용
            SocketAsyncEventArgs recv_args = _recvArgs_pool.Pop();
            SocketAsyncEventArgs send_args = _sendArgs_pool.Pop();

            if (_onSessionCreated_callback != null)
            {
                CUserToken userToken = recv_args.UserToken as CUserToken;
                _onSessionCreated_callback.Invoke(userToken);
            }

            BeginReceive(clientSocket, recv_args, send_args);
        }

        void BeginReceive(Socket clientSocket, SocketAsyncEventArgs recv_args, SocketAsyncEventArgs send_args)
        {
            // recv 혹은 send 중 아무거나 꺼내와도 상관 없다? 둘다 같은 토큰이 물려 있음
            CUserToken token = recv_args.UserToken as CUserToken;
            token.SetEventArgs(recv_args, send_args);

            // 생성된 클라이언트 소켓을 보관해 놓고 통신할 때 사용한다.
            token._socket = clientSocket;

            // 데이터를 받을 수 있도록 소켓 메소드를 호출해준다.
            // 비동기로 수신할 경우 worker thread에서 대기중으로 있다가 Completed에 설정해놓은 메소드가 호출된다.
            // 동기로 완료될 경우에는 직접 완료 메소드를 호출해줘야 한다.
            bool pending = clientSocket.ReceiveAsync(recv_args);
            if(pending == false)
            {
                ProcessReceive(recv_args);
            }
        }

        void ProcessReceive(SocketAsyncEventArgs recv_args)
        {
            CUserToken token = recv_args.UserToken as CUserToken;
            if (recv_args.BytesTransferred > 0 && recv_args.SocketError == SocketError.Success)
            {
                // 이후의 작업은 CUserToken에게 맡긴다. 매우 중요
                token.OnReceive(recv_args.Buffer, recv_args.Offset, recv_args.BytesTransferred);


                // 다음 메세지 수신 시도
                bool pending = token._socket.ReceiveAsync(recv_args);
                if (pending == false)
                {
                    ProcessReceive(recv_args);
                }
            }
            else
            {
                Console.WriteLine(string.Format($"error {recv_args.SocketError}, transferred {recv_args.BytesTransferred}"));
                CloseClientSocket(token);
            }
        }

        void OnRecvCompleted(object sender, SocketAsyncEventArgs args)
        {
            if(args.LastOperation == SocketAsyncOperation.Receive)
            {
                ProcessReceive(args);
                return;
            }

            throw new ArgumentException("The last operation completed on the socket was not a receive.");
        }
        
        void CloseClientSocket(CUserToken token)
        {

        }
    }
}
