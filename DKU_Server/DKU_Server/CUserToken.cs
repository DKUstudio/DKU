using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server
{
    public class CUserToken
    {
        public Socket _socket { get; set; }
        SocketAsyncEventArgs _recvArgs;
        SocketAsyncEventArgs _sendArgs;

        MessageResolver _messageResolver;
        Queue<CPacket> _sendingQueue = new Queue<CPacket>();
        

        public void SetEventArgs(SocketAsyncEventArgs recv_args, SocketAsyncEventArgs send_args)
        {
            _recvArgs = recv_args;
            _sendArgs = send_args;
        }



        public void OnReceive(byte[] buffer, int offset, int bytesTransferred)
        {
            _messageResolver.OnReceive(buffer, offset, bytesTransferred, OnMessage);
        }

        void OnMessage(byte[] message)
        {

        }

        public void Send(CPacket msg)
        {
            lock(_sendingQueue)
            {
                // 큐가 비어있다면 바로 추가하고 비동기 전송 메소드를 호출한다.
                if(_sendingQueue.Count <= 0)
                {
                    _sendingQueue.Enqueue(msg);
                    StartSend();
                    return;
                }
                _sendingQueue.Enqueue(msg);

            }
        }

        void StartSend()
        {
            lock(_sendingQueue)
            {
                CPacket msg = _sendingQueue.Peek();

                // 헤더에 패킷 사이즈를 기록
                msg.RecordSize();

                // 이번에 보낼 패킷 사이즈만큼 버퍼 크기를 설정
                _sendArgs.SetBuffer(_sendArgs.Offset, msg.position);
                // 패킷 내용을 SocketAsyncEventArgs 버퍼에 복사
                Array.Copy(msg.buffer, 0, _sendArgs.Buffer, _sendArgs.Offset, msg.position);

                bool pending = _socket.SendAsync(_sendArgs);
                if(pending == false)
                {
                    ProcessSend(_sendArgs);
                }
            }
        }

        void ProcessSend(SocketAsyncEventArgs args)
        {
            CPacket packet = _sendingQueue.Dequeue();

            if(_sendingQueue.Count > 0)
            {
                StartSend();
            }
        }
    }
}
