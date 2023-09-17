using DKU_Server.UserToken;
using DKU_ServerCore;
using DKU_ServerCore.UserToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClinet
{
    public class Network
    {
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

        void onRecvCompleted(object sender, EventArgs e)
        { 

        }
    }
}
