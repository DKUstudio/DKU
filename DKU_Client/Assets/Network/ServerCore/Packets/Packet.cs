using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore.Packets
{

    public class Packet
    {
        // 해당 패킷의 타입 데이터, enum PacketType
        public short m_type { get; set; }
        // 해당 타입의 직렬화된 데이터
        public byte[] m_data { get; set; }

        public Packet() { }
        public Packet(PacketType type, byte[] data, int len)
        {
            SetData(type, data, len);
        }

        // 클래스를 직렬화한 데이터를 받아옴
        public void SetData(byte[] data, int len)
        {
            // byte 배열 초기화
            m_data = new byte[len];
            Array.Copy(data, m_data, len);
        }

        public void SetData(PacketType type, byte[] data, int len)
        {
            m_type = (short)type;
            m_data = new byte[len];
            Array.Copy(data, m_data, len);
        }

        // 클래스를 직렬화한 데이터를 tcp 통신에 맞는 형태로 변환해줌.
        public byte[] GetSendBytes()
        {
            byte[] type_bytes = BitConverter.GetBytes(m_type);          // 타입을 byte 배열로 변환
            int header_size = m_data.Length;                     // 데이터 크기 정보
            byte[] header_bytes = BitConverter.GetBytes(header_size);   // 데이터 크기 정보를 byte 배열로 변환

            // return::직렬화
            byte[] send_bytes = new byte[header_bytes.Length + type_bytes.Length + m_data.Length];

            // 헤더 복사, 헤더==데이터의 크기, tcp 통신을 위해 앞에 붙여줌
            Array.Copy(header_bytes, 0, 
                send_bytes, 0,
                header_bytes.Length);

            // 타입 복사
            Array.Copy(type_bytes, 0, 
                send_bytes, header_bytes.Length, 
                type_bytes.Length);

            // 데이터 복사
            Array.Copy(m_data, 0, 
                send_bytes, header_bytes.Length + type_bytes.Length, 
                m_data.Length);

            // tcp용 패킷 래퍼 완료
            return send_bytes;
        }
    }
}
