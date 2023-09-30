
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_WaitingIdRes_Handler
{
    public static void Method(Packet packet)
    {
        S_WaitingIdRes res = Data<S_WaitingIdRes>.Deserialize(packet.m_data);

    }
}
