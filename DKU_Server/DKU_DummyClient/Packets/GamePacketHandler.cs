
using DKU_DummyClient.Packets.var;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClient.Packets
{
    public class GamePacketHandler
    {
        public void ParsePacket(Packet packet)
        {
            switch ((PacketType)packet.m_type)
            {

                case PacketType.S_ChatRes:
                    S_ChatRes_Impl(packet);
                    break;

                case PacketType.S_LogoutRes:
                    S_LogoutRes_Impl(packet);
                    break;

                case PacketType.S_PingRes:
                    S_PingRes_Impl(packet);
                    break;

                case PacketType.S_StartAuthRes:
                    S_StartAuthRes_Impl(packet);
                    break;

                case PacketType.S_TryAuthRes:
                    S_TryAuthRes_Impl(packet);
                    break;

                case PacketType.S_UserPosRes:
                    S_UserPosRes_Impl(packet);
                    break;

            default:
                break;
            }
        }

        void S_ChatRes_Impl(Packet packet)
        {
            S_ChatRes_Handler.Method(packet);
        }

        void S_LogoutRes_Impl(Packet packet)
        {
            S_LogoutRes_Handler.Method(packet);
        }

        void S_PingRes_Impl(Packet packet)
        {
            S_PingRes_Handler.Method(packet);
        }

        void S_StartAuthRes_Impl(Packet packet)
        {
            S_StartAuthRes_Handler.Method(packet);
        }

        void S_TryAuthRes_Impl(Packet packet)
        {
            S_TryAuthRes_Handler.Method(packet);
        }

        void S_UserPosRes_Impl(Packet packet)
        {
            S_UserPosRes_Handler.Method(packet);
        }

    }
}
