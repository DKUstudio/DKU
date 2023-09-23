using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server
{
    public class GamePacketHandler
    {

        /*public void Init(Network network)
        {
            m_network = network;
        }*/

        public void ParsePacket(Packet packet)
        {
            switch ((PacketType)packet.m_type)
            {
                case PacketType.GlobalChatReq:
                    GlobalChatReq_Handler(packet);
                    break;

                case PacketType.LogoutReq:
                    LogoutReq_Handler(packet);
                    break;

            }
        }

        /* public void TestPacketRes(Packet packet)
         {
             // 역직렬화해서 원래 데이터로 만든다.
             TestPacketRes notify = Data<TestPacketRes>.Deserialize(packet.m_data);
         }*/

        void GlobalChatReq_Handler(Packet packet)
        {
            GlobalChatReq req = Data<GlobalChatReq>.Deserialize(packet.m_data);

            Console.WriteLine("recved: " + req.chat_message);
            GlobalChatRes res = new GlobalChatRes();
            res.chat_message = req.chat_message;
            byte[] serial = res.Serialize();
            //Console.WriteLine(CommonDefine.ToReadableByteArray(serial));

            Packet resPkt = new Packet();
            resPkt.SetData(serial, serial.Length);
            resPkt.m_type = (int)PacketType.GlobalChatRes;

            foreach (var targets in NetworkManager.Instance.tokens)
            {
                targets.Value.Token.Send(resPkt);
            }
        }

        void LogoutReq_Handler(Packet packet)
        {
            LogoutReq req = Data<LogoutReq>.Deserialize(packet.m_data);
            NetworkManager.Instance.tokens.Remove(req.uid);
        }
    }
}
