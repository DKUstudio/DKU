using DKU_DummyClient;
using DKU_DummyClinet.Packets;
using DKU_ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClinet
{
    public class GamePacketHandler
    {
        Network m_network;

        public void Init(Network network)
        {
            m_network = network;
        }

        public void ParsePacket(Packet packet)
        {
            switch((PacketType)packet.m_type)
            {
                case PacketType.TEST_TYPE_1:
                    TestPacketRes(packet);
                    break;


            }
        }

        public void TestPacketRes(Packet packet)
        {
            // 역직렬화해서 원래 데이터로 만든다.
            TestPacketRes notify = Data<TestPacketRes>.Deserialize(packet.m_data);
        }
    }
}
