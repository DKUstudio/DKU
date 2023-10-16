using DKU_Server.Connections.Tokens;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Connections
{
    public class SMessageResolver : MessageResolver
    {
        public UserToken? userToken;

        public void onRecv(byte[] buffer, int offset, int transffered, Action<SPacket> onComplete)
        {
            // 현재 들어온 데이터의 위치를 저장한다.
            // 원본 byte 배열에서의 offset이 eventArgs로 날아오는듯?
            int src_position = offset;

            // 메시지가 완성되었다면, 콜백함수를 호출해준다.
            //m_complete_callback = onComplete;

            // 처리해야 할 메시지 양을 저장한다.
            m_remain_bytes = transffered;

            // 헤더가 완성되지 않은 상태 (패킷의 길이 데이터)
            if (m_head_completed == false)
            {
                // 패킷의 헤더 데이터를 완성하지 않았다면, 읽어 온 데이터로 헤더를 완성한다.
                m_head_completed = readHead(buffer, ref src_position);

                // 읽어 온 데이터로도 헤더를 완성하지 못했다면, 다음 데이터 전송을 기다린다.
                if (m_head_completed == false)
                    return;

                // 헤더를 완성했으면, 헤더 정보에 있는 데이터의 전체 양을 확인한다.
                m_message_size = getBodySize();

                // 잘못된 데이터인지 확인하는 코드이다.
                // 현재 20K까지만 받을 수 있다.
                if (m_message_size < 0 ||
                    m_message_size > CommonDefine.COMPLETE_MESSAGE_SIZE_CLIENT)
                    return;
            }

            // 타입이 완성되지 않은 상태 (클래스 enum 정보)
            if (m_type_completed == false)
            {
                // 남은 데이터가 있다면, 타입 정보를 완성한다.
                m_type_completed = readType(buffer, ref src_position);

                // 타입 정보를 완성하지 못했다면, 다음 메시지 전송을 기다린다.
                if (m_type_completed == false)
                    return;

                // 타입 정보를 완성했다면, 패킷 타입을 정의한다. (enum type)
                m_message_type = BitConverter.ToInt16(m_type_buffer, 0);

                // 잘못된 데이터인지 확인하는 코드이다.
                // 현재 20K까지만 받을 수 있다.
                if (m_message_type < 0 ||
                    m_message_type >= (int)PacketType.PACKET_COUNT)
                    return;

                // 데이터가 미완성일 경우, 다음에 전송되었을 때를 위해 저장해둔다.
                m_pre_type = (PacketType)m_message_type;
            }

            if (m_completed == false)
            {
                // 남은 데이터가 있다면, 데이터 완성 과정을 진행한다.
                m_completed = readBody(buffer, ref src_position);
                if (m_completed == false)
                    return;
            }

            // 데이터가 완성되었다면 여기 도달하고, 패킷으로 만들게 된다.
            SPacket packet = new SPacket();
            packet.m_type = m_message_type;
            packet.SetData(m_message_buffer, m_message_size);
            packet.SetUserToken(userToken);

            // 패킷이 완성되었음을 알린다.
            onComplete.Invoke(packet);

            // 패킷을 만드는데 사용한 버퍼를 초기화해준다.
            ClearBuffer();

        }
    }
}
