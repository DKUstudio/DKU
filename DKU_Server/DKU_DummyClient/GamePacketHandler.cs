using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClient
{
    public class GamePacketHandler
    {
        public void ParsePacket(Packet packet)
        {
            switch ((PacketType)packet.m_type)
            {
                case PacketType.S_AcceptIdRes:
                    S_AcceptIdRes_Handler(packet);
                    break;

                case PacketType.S_RegisterRes:
                    S_RegisterRes_Handler(packet);
                    break;

                case PacketType.S_LoginRes:
                    S_LoginRes_Handler(packet);
                    break;

                case PacketType.S_LogoutRes:
                    S_LogoutRes_Handler(packet);
                    break;

                case PacketType.S_GlobalChatRes:
                    //TestPacketRes(packet);
                    S_GlobalChatRes_Handler(packet);
                    break;


            }
        }

        void S_AcceptIdRes_Handler(Packet packet)
        {
            S_AcceptIdRes res = Data<S_AcceptIdRes>.Deserialize(packet.m_data);
            Network.Instance.m_accept_id = res.accept_id;
        }
        void S_RegisterRes_Handler(Packet packet)
        {
            S_RegisterRes res = Data<S_RegisterRes>.Deserialize(packet.m_data);
            if(res.success)
            {
                // 성공
                Console.WriteLine("회원가입 성공");
            }
            else
            {
                // 실패
                Console.WriteLine("회원가입 실패");
            }
        }

        void S_LoginRes_Handler(Packet packet)
        {
            S_LoginRes res = Data<S_LoginRes>.Deserialize(packet.m_data);
            if (res.success)
            {
                // 성공
                Console.WriteLine("로그인 성공");
                Network.Instance.m_user_data = res.udata;
            }
            else
            {
                // 실패
                Console.WriteLine("로그인 실패");
            }
        }

        void S_LogoutRes_Handler(Packet packet)
        {
            Environment.Exit(0);
        }

        void S_GlobalChatRes_Handler(Packet packet)
        {
            S_GlobalChatRes res = Data<S_GlobalChatRes>.Deserialize(packet.m_data);
            Console.WriteLine($"[{res.udata.nickname}]: {res.chat_message}");
        }

    }
}
