
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

            case PacketType.S_LoginRes:
                S_LoginRes_Impl(packet);
                break;

            case PacketType.S_LogoutRes:
                S_LogoutRes_Impl(packet);
                break;

            case PacketType.S_PingRes:
                S_PingRes_Impl(packet);
                break;

            case PacketType.S_RegisterRes:
                S_RegisterRes_Impl(packet);
                break;

            case PacketType.S_WaitingIdRes:
                S_WaitingIdRes_Impl(packet);
                break;

        }
    }

    void S_ChatRes_Impl(Packet packet)
    {
        S_ChatRes_Handler.Method(packet);
    }

    void S_LoginRes_Impl(Packet packet)
    {
        S_LoginRes_Handler.Method(packet);
    }

    void S_LogoutRes_Impl(Packet packet)
    {
        S_LogoutRes_Handler.Method(packet);
    }

    void S_PingRes_Impl(Packet packet)
    {
        S_PingRes_Handler.Method(packet);
    }

    void S_RegisterRes_Impl(Packet packet)
    {
        S_RegisterRes_Handler.Method(packet);
    }

    void S_WaitingIdRes_Impl(Packet packet)
    {
        S_WaitingIdRes_Handler.Method(packet);
    }

}
