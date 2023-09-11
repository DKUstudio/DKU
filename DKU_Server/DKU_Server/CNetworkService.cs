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

        void OnNewClient(Socket socket, object o)
        {
            // 풀에서 꺼내와 사용
            SocketAsyncEventArgs recv_args = _recvArgs_pool.Pop();
            SocketAsyncEventArgs send_args = _sendArgs_pool.Pop();

            
        }
    }
}
