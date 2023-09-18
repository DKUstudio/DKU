using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DKU_DummyClinet;
using DKU_ServerCore;

namespace DKU_DummyClient
{
    public class Network
    {
        Socket m_socket;

        SocketAsyncEventArgs m_recv_event_args;
        MessageResolver m_message_resolver;
        LinkedList<Packet> m_recv_packet_list;
        GamePacketHandler m_game_packet_handler;
        byte[] m_recv_buffer;

        public void Init()
        {
            // 받은 byte 배열을 패킷으로 만들어, 리스트에 넣고 GamePacketHandler에서 처리한다.
            m_recv_packet_list = new LinkedList<Packet>();
            m_recv_buffer = new byte[CommonDefine.SOCKET_BUFFER_SIZE];
            m_message_resolver = new MessageResolver();

            // 전송된 패킷을 처리하는 부분이다.
            m_game_packet_handler = new GamePacketHandler();
            m_game_packet_handler.Init(this);

            // 메세지를 받을 버퍼를 설정한다. 여기서는 4K.
            // 만약 패킷 크기가 4K보다 큰 10K라면, [4K,4K,2K]로 3번에 나누어 이벤트가 발생한다.
            m_recv_event_args = new SocketAsyncEventArgs();
            m_recv_event_args.Completed += onRecvCompleted;
            m_recv_event_args.UserToken = this;
            m_recv_event_args.SetBuffer(m_recv_buffer, 0, 1024 * 4);
        }

        public void Connect(string v_address, int v_port)
        {
            // tcp connect
            m_socket = new Socket(AddressFamily.InterNetworkV6,
                SocketType.Stream, ProtocolType.Tcp);

            // 버퍼에 데이터를 쌓아서 한번에 전송하는게 아니라, 그때그때 전송한다.
            // 이렇게 하지 않으면, 렉이 생긴다.
            m_socket.NoDelay = true;

            // 연결할 서버의 ip 및 포트 설정
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(v_address), v_port);

            // 비동기 접속을 위한 event args.
            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.Completed += onConnected;
            args.RemoteEndPoint = endPoint;

            bool pending = m_socket.ConnectAsync(args);

            if(!pending)
                onConnected(null, args);
        }

        void onConnected(object sender, SocketAsyncEventArgs e)
        {
            if(e.SocketError == SocketError.Success)
            {
                // 연결 성공
                Console.WriteLine("[Client] Connected to Server!");
            }
            else
            {
                // 연결 실패
                Console.WriteLine("[Client] Failed to connect Server");
            }
        }

        #region send
        public void Send(Packet packet)
        {
            // 소켓에 연결이 안된 상태라면, 종료한다.
            if(m_socket == null || !m_socket.Connected) 
                return;

            // 네트워크 전송이 빈번해 SocketAsyncEventArgs 객체를 풀로 만들어 쓴다.
            SocketAsyncEventArgs send_event_args = SocketAsyncEventArgsPool.Instance.Pop();
            if(send_event_args == null)
            {
                Console.WriteLine("SocketAsyncEventArgsPool::Pop() result is null");
                return;
            }

            // 전송할 데이터 byte 배열을 SocketAsyncEventArgs 객체 버퍼에 복사한다.
            byte[] send_data = packet.GetSendBytes();
            send_event_args.SetBuffer(send_data, 0, send_data.Length);

            // 앞서 연결과 마찬가지로, 비동기 함수로 SocketAsyncEventArgs 객체를 전송한다.
            bool pending = m_socket.SendAsync(send_event_args);
            if(!pending)
                onSendCompleted(null, send_event_args);

        }

        void onSendCompleted(object sender, SocketAsyncEventArgs e)
        {
            if(e.SocketError == SocketError.Success)
            {
                // 전송 성공
                Console.WriteLine("[Client] Send to Server Complete.");
            }
            else
            {
                // 전송 실패
            }

            // 사용했던 SocketAsyncEventArgs를 다시 풀에 넣어준다.
            // 넣기 전에 해당 객체를 초기화 시켜준다.
            e.Completed -= onSendCompleted;
            SocketAsyncEventArgsPool.Instance.Push(e);
        }
        #endregion

        #region recv
        public void StartRecv()
        {
            // 서버에 연결된 상태에서 이 함수를 호출해, 메시지가 오기를 기다리자.
            // 메시지가 오면 onRecvCompleted 콜백함수가 호출된다.
            bool pending = m_socket.ReceiveAsync(m_recv_event_args);
            if (!pending)
                onRecvCompleted(null, m_recv_event_args);

        }

        void onRecvCompleted(object sender, SocketAsyncEventArgs e)
        {
            if(e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                // 수신 성공
                // byte 배열의 데이터를 다시 패킷으로 만들어준다.
                m_message_resolver.onRecv(e.Buffer, e.Offset, e.BytesTransferred, onMessageCompleted);

                // 새로운 메시지 수신 시작
                StartRecv();
            }
            else
            {
                // 실패
            }
        }

        void onMessageCompleted(Packet packet)
        {
            // 패킷 리스트에 넣는다.
            PushPacket(packet);   
        }

        // 수신한 패킷들을 리스트에 저장해둠.
        void PushPacket(Packet packet)
        {
            // 패킷 완성하는 메인 스레드가 아닐 수도 있다.
            // 서로 다른 스레드인 경우가 있어, 락을 건다.
            lock(m_recv_packet_list)
            {
                m_recv_packet_list.AddLast(packet);
            }
        }

        // 패킷 리스트에 담긴 내용들을 해석하고 처리함.
        public void ProcessPackets()
        {
            lock(m_recv_packet_list)
            {
                foreach (Packet packet in m_recv_packet_list)
                    m_game_packet_handler.ParsePacket(packet);
                m_recv_packet_list.Clear();
            }
        }
        #endregion
    }
}
