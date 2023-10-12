
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
