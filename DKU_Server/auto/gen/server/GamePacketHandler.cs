
using DKU_Server.Connections;
using DKU_Server.Connections.Tokens;
using DKU_Server.Worlds;
using DKU_Server.Packets.var;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Packets
{
    public class GamePacketHandler
    {
        public void ParsePacket(Packet packet)
        {
            switch ((PacketType)packet.m_type)
            {

                case PacketType.C_GlobalChatReq:
                    C_GlobalChatReq_Impl(packet);
                    break;

                case PacketType.C_LoginReq:
                    C_LoginReq_Impl(packet);
                    break;

                case PacketType.C_LogoutReq:
                    C_LogoutReq_Impl(packet);
                    break;

                case PacketType.C_PingReq:
                    C_PingReq_Impl(packet);
                    break;

                case PacketType.C_RegisterReq:
                    C_RegisterReq_Impl(packet);
                    break;

            }
        }

        void C_GlobalChatReq_Impl(Packet packet)
        {
            C_GlobalChatReq_Handler.Method(packet);
        }

        void C_LoginReq_Impl(Packet packet)
        {
            C_LoginReq_Handler.Method(packet);
        }

        void C_LogoutReq_Impl(Packet packet)
        {
            C_LogoutReq_Handler.Method(packet);
        }

        void C_PingReq_Impl(Packet packet)
        {
            C_PingReq_Handler.Method(packet);
        }

        void C_RegisterReq_Impl(Packet packet)
        {
            C_RegisterReq_Handler.Method(packet);
        }

    }
}
