
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
        public void ParsePacket(SPacket packet)
        {
            switch ((PacketType)packet.m_type)
            {

                case PacketType.C_ChatReq:
                    C_ChatReq_Impl(packet);
                    break;

                case PacketType.C_GetWorldUsersDataReq:
                    C_GetWorldUsersDataReq_Impl(packet);
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

                case PacketType.C_TryAuthReq:
                    C_TryAuthReq_Impl(packet);
                    break;

                case PacketType.C_UserCharaDataBitChangeReq:
                    C_UserCharaDataBitChangeReq_Impl(packet);
                    break;

                case PacketType.C_UserCharaDataChangeReq:
                    C_UserCharaDataChangeReq_Impl(packet);
                    break;

                case PacketType.C_UserCharaDataReq:
                    C_UserCharaDataReq_Impl(packet);
                    break;

                case PacketType.C_UserCharaDataShiftChangeReq:
                    C_UserCharaDataShiftChangeReq_Impl(packet);
                    break;

                case PacketType.C_UserPosReq:
                    C_UserPosReq_Impl(packet);
                    break;

                case PacketType.C_WorldChangeAvailReq:
                    C_WorldChangeAvailReq_Impl(packet);
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

        void C_ChatReq_Impl(SPacket packet)
        {
            C_ChatReq_Handler.Method(packet);
        }

        void C_GetWorldUsersDataReq_Impl(SPacket packet)
        {
            C_GetWorldUsersDataReq_Handler.Method(packet);
        }

        void C_LoginReq_Impl(SPacket packet)
        {
            C_LoginReq_Handler.Method(packet);
        }

        void C_LogoutReq_Impl(SPacket packet)
        {
            C_LogoutReq_Handler.Method(packet);
        }

        void C_MyUserDataReq_Impl(SPacket packet)
        {
            C_MyUserDataReq_Handler.Method(packet);
        }

        void C_PingReq_Impl(SPacket packet)
        {
            C_PingReq_Handler.Method(packet);
        }

        void C_RegisterReq_Impl(SPacket packet)
        {
            C_RegisterReq_Handler.Method(packet);
        }

        void C_StartAuthReq_Impl(SPacket packet)
        {
            C_StartAuthReq_Handler.Method(packet);
        }

        void C_TryAuthReq_Impl(SPacket packet)
        {
            C_TryAuthReq_Handler.Method(packet);
        }

        void C_UserCharaDataBitChangeReq_Impl(SPacket packet)
        {
            C_UserCharaDataBitChangeReq_Handler.Method(packet);
        }

        void C_UserCharaDataChangeReq_Impl(SPacket packet)
        {
            C_UserCharaDataChangeReq_Handler.Method(packet);
        }

        void C_UserCharaDataReq_Impl(SPacket packet)
        {
            C_UserCharaDataReq_Handler.Method(packet);
        }

        void C_UserCharaDataShiftChangeReq_Impl(SPacket packet)
        {
            C_UserCharaDataShiftChangeReq_Handler.Method(packet);
        }

        void C_UserPosReq_Impl(SPacket packet)
        {
            C_UserPosReq_Handler.Method(packet);
        }

        void C_WorldChangeAvailReq_Impl(SPacket packet)
        {
            C_WorldChangeAvailReq_Handler.Method(packet);
        }

        void Q_GoToGameServerRes_Impl(SPacket packet)
        {
            Q_GoToGameServerRes_Handler.Method(packet);
        }

        void Q_LoginRes_Impl(SPacket packet)
        {
            Q_LoginRes_Handler.Method(packet);
        }

        void Q_QueueStartRes_Impl(SPacket packet)
        {
            Q_QueueStartRes_Handler.Method(packet);
        }

        void Q_RegisterRes_Impl(SPacket packet)
        {
            Q_RegisterRes_Handler.Method(packet);
        }

        void Q_WaitForLoginRes_Impl(SPacket packet)
        {
            Q_WaitForLoginRes_Handler.Method(packet);
        }

        void Q_YourWidRes_Impl(SPacket packet)
        {
            Q_YourWidRes_Handler.Method(packet);
        }

    }
}
