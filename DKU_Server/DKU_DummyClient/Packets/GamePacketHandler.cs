
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

                case PacketType.S_ConnectionTimeoutRes:
                    S_ConnectionTimeoutRes_Impl(packet);
                    break;

                case PacketType.S_PingRes:
                    S_PingRes_Impl(packet);
                    break;

                case PacketType.S_PlayerPosRes:
                    S_PlayerPosRes_Impl(packet);
                    break;

                case PacketType.S_StartAuthRes:
                    S_StartAuthRes_Impl(packet);
                    break;

                case PacketType.S_TryAuthRes:
                    S_TryAuthRes_Impl(packet);
                    break;

            default:
                break;
            }
        }

        void S_ChatRes_Impl(SPacket packet)
        {
            S_ChatRes_Handler.Method(packet);
        }

        void S_ConnectionTimeoutRes_Impl(SPacket packet)
        {
            S_ConnectionTimeoutRes_Handler.Method(packet);
        }

        void S_PingRes_Impl(SPacket packet)
        {
            S_PingRes_Handler.Method(packet);
        }

        void S_PlayerPosRes_Impl(SPacket packet)
        {
            S_PlayerPosRes_Handler.Method(packet);
        }

        void S_StartAuthRes_Impl(SPacket packet)
        {
            S_StartAuthRes_Handler.Method(packet);
        }

        void S_TryAuthRes_Impl(SPacket packet)
        {
            S_TryAuthRes_Handler.Method(packet);
        }

    }
}
