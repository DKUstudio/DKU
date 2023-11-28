
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            case PacketType.S_GetWorldUsersDataRes:
                S_GetWorldUsersDataRes_Impl(packet);
                break;

            case PacketType.S_OtherUserAnimChangeRes:
                S_OtherUserAnimChangeRes_Impl(packet);
                break;

            case PacketType.S_OtherUserCharShiftChangedRes:
                S_OtherUserCharShiftChangedRes_Impl(packet);
                break;

            case PacketType.S_OtherUserLoginRes:
                S_OtherUserLoginRes_Impl(packet);
                break;

            case PacketType.S_OtherUserLogoutRes:
                S_OtherUserLogoutRes_Impl(packet);
                break;

            case PacketType.S_OXGameStartRes:
                S_OXGameStartRes_Impl(packet);
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

            case PacketType.S_UserCharaDataLoginRes:
                S_UserCharaDataLoginRes_Impl(packet);
                break;

            case PacketType.S_UserCharaDataRes:
                S_UserCharaDataRes_Impl(packet);
                break;

            case PacketType.S_UserPosRes:
                S_UserPosRes_Impl(packet);
                break;

            case PacketType.S_WorldChangeAvailRes:
                S_WorldChangeAvailRes_Impl(packet);
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

    void S_ChatRes_Impl(Packet packet)
    {
        S_ChatRes_Handler.Method(packet);
    }

    void S_ConnectionTimeoutRes_Impl(Packet packet)
    {
        S_ConnectionTimeoutRes_Handler.Method(packet);
    }

    void S_GetWorldUsersDataRes_Impl(Packet packet)
    {
        S_GetWorldUsersDataRes_Handler.Method(packet);
    }

    void S_OtherUserAnimChangeRes_Impl(Packet packet)
    {
        S_OtherUserAnimChangeRes_Handler.Method(packet);
    }

    void S_OtherUserCharShiftChangedRes_Impl(Packet packet)
    {
        S_OtherUserCharShiftChangedRes_Handler.Method(packet);
    }

    void S_OtherUserLoginRes_Impl(Packet packet)
    {
        S_OtherUserLoginRes_Handler.Method(packet);
    }

    void S_OtherUserLogoutRes_Impl(Packet packet)
    {
        S_OtherUserLogoutRes_Handler.Method(packet);
    }

    void S_OXGameStartRes_Impl(Packet packet)
    {
        S_OXGameStartRes_Handler.Method(packet);
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

    void S_UserCharaDataLoginRes_Impl(Packet packet)
    {
        S_UserCharaDataLoginRes_Handler.Method(packet);
    }

    void S_UserCharaDataRes_Impl(Packet packet)
    {
        S_UserCharaDataRes_Handler.Method(packet);
    }

    void S_UserPosRes_Impl(Packet packet)
    {
        S_UserPosRes_Handler.Method(packet);
    }

    void S_WorldChangeAvailRes_Impl(Packet packet)
    {
        S_WorldChangeAvailRes_Handler.Method(packet);
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
