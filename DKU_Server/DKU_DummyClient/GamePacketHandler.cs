using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClient
{
    public class GamePacketHandler
    {
        Network m_network;

        public GamePacketHandler(Network network)
        {
            this.m_network = network;
        }

        /*public void Init(Network network)
        {
            m_network = network;
        }*/

        public void ParsePacket(Packet packet)
        {
            switch ((PacketType)packet.m_type)
            {
                case PacketType.GlobalChatRes:
                    //TestPacketRes(packet);
                    GlobalChatRes_Handler(packet);
                    break;

                case PacketType.UserDataRes:
                    UserDataRes_Handler(packet);
                    break;

            }
        }

        /*public void TestPacketRes(Packet packet)
        {
            // 역직렬화해서 원래 데이터로 만든다.
            TestPacketRes notify = Data<TestPacketRes>.Deserialize(packet.m_data);
        }*/

        void GlobalChatRes_Handler(Packet packet)
        {
            //Console.WriteLine(CommonDefine.ToReadableByteArray(packet.m_data));
            GlobalChatRes res = Data<GlobalChatRes>.Deserialize(packet.m_data);
            Console.WriteLine(res.chat_message);
        }

        void UserDataRes_Handler(Packet packet)
        {
            UserDataRes res = Data<UserDataRes>.Deserialize(packet.m_data);
            m_network.m_user_data = res.user_data;
        }
    }
}
