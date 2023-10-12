
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

                case PacketType.C_ChatReq:
                    C_ChatReq_Impl(packet);
                    break;

                case PacketType.C_LoginReq:
                    C_LoginReq_Impl(packet);
                    break;

                case PacketType.C_LogoutReq:
                    C_LogoutReq_Impl(packet);
                    break;

                case PacketType.C_MyUserDataReq:
                    C_MyUserDataReq_Impl(packet);
                    break;

                case PacketType.C_PingReq:
                    C_PingReq_Impl(packet);
                    break;

                case PacketType.C_RegisterReq:
                    C_RegisterReq_Impl(packet);
                    break;

                case PacketType.C_StartAuthReq:
                    C_StartAuthReq_Impl(packet);
                    break;

                case PacketType.C_StopWaitingReq:
                    C_StopWaitingReq_Impl(packet);
                    break;

                case PacketType.C_TryAuthReq:
                    C_TryAuthReq_Impl(packet);
                    break;

                case PacketType.C_UserPosReq:
                    C_UserPosReq_Impl(packet);
                    break;

                case PacketType.Q_CurUsersCountReq:
                    Q_CurUsersCountReq_Impl(packet);
                    break;

                case PacketType.Q_GoToGameServerRes:
                    Q_GoToGameServerRes_Impl(packet);
                    break;

                case PacketType.Q_LoginRes:
                    Q_LoginRes_Impl(packet);
                    break;

                case PacketType.Q_QueueStartRes:
                    Q_QueueStartRes_Impl(packet);
                    break;

                case PacketType.Q_RegisterRes:
                    Q_RegisterRes_Impl(packet);
                    break;

                case PacketType.Q_WaitForLoginRes:
                    Q_WaitForLoginRes_Impl(packet);
                    break;

                case PacketType.Q_YourWidRes:
                    Q_YourWidRes_Impl(packet);
                    break;

            default:
                break;
            }
        }

        void C_ChatReq_Impl(Packet packet)
        {
            C_ChatReq_Handler.Method(packet);
        }

        void C_LoginReq_Impl(Packet packet)
        {
            C_LoginReq_Handler.Method(packet);
        }

        void C_LogoutReq_Impl(Packet packet)
        {
            C_LogoutReq_Handler.Method(packet);
        }

        void C_MyUserDataReq_Impl(Packet packet)
        {
            C_MyUserDataReq_Handler.Method(packet);
        }

        void C_PingReq_Impl(Packet packet)
        {
            C_PingReq_Handler.Method(packet);
        }

        void C_RegisterReq_Impl(Packet packet)
        {
            C_RegisterReq_Handler.Method(packet);
        }

        void C_StartAuthReq_Impl(Packet packet)
        {
            C_StartAuthReq_Handler.Method(packet);
        }

        void C_StopWaitingReq_Impl(Packet packet)
        {
            C_StopWaitingReq_Handler.Method(packet);
        }

        void C_TryAuthReq_Impl(Packet packet)
        {
            C_TryAuthReq_Handler.Method(packet);
        }

        void C_UserPosReq_Impl(Packet packet)
        {
            C_UserPosReq_Handler.Method(packet);
        }

        void Q_CurUsersCountReq_Impl(Packet packet)
        {
            Q_CurUsersCountReq_Handler.Method(packet);
        }

        void Q_GoToGameServerRes_Impl(Packet packet)
        {
            Q_GoToGameServerRes_Handler.Method(packet);
        }

        void Q_LoginRes_Impl(Packet packet)
        {
            Q_LoginRes_Handler.Method(packet);
        }

        void Q_QueueStartRes_Impl(Packet packet)
        {
            Q_QueueStartRes_Handler.Method(packet);
        }

        void Q_RegisterRes_Impl(Packet packet)
        {
            Q_RegisterRes_Handler.Method(packet);
        }

        void Q_WaitForLoginRes_Impl(Packet packet)
        {
            Q_WaitForLoginRes_Handler.Method(packet);
        }

        void Q_YourWidRes_Impl(Packet packet)
        {
            Q_YourWidRes_Handler.Method(packet);
        }

    }
}
