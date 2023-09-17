using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore
{
    public enum PacketType
    {
        TYPE_NONE = -1,

        TEST_TYPE_1,
        TEST_TYPE_2,
        TEST_TYPE_3,

        PACKET_COUNT
    }

    public class Packet
    {
        public Int16 m_type { get; set; }
        public byte[] m_data { get; set; }

        public void SetData(byte[] data, int len)
        {
            m_data = new byte[len];
            Array.Copy(data, m_data, len);
        }

        public byte[] GetSendBytes()
        {
            byte[] type_bytes = BitConverter.GetBytes(m_type);
            int header_size = (int)(m_data.Length);
            byte[] header_bytes = BitConverter.GetBytes(header_size);
            byte[] send_bytes = new byte[header_bytes.Length + type_bytes.Length];

            // 헤더 복사, 헤더==데이터의 크기
            Array.Copy(header_bytes, 0, send_bytes, 0, header_bytes.Length);

            // 타입 복사
            Array.Copy(type_bytes, 0, send_bytes, send_bytes.Length, type_bytes.Length);

            // 데이터 복사
            Array.Copy(m_data, 0, send_bytes, header_size + type_bytes.Length, m_data.Length);

            return send_bytes;
        }
    }
}
