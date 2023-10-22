
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_ConnectionTimeoutRes_Handler
{
    public static void Method(Packet packet)
    {
        S_ConnectionTimeoutRes res = Data<S_ConnectionTimeoutRes>.Deserialize(packet.m_data);

    }
}
