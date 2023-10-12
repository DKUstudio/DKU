
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class Q_RegisterRes_Handler
{
    public static void Method(Packet packet)
    {
        Q_RegisterRes res = Data<Q_RegisterRes>.Deserialize(packet.m_data);

    }
}
