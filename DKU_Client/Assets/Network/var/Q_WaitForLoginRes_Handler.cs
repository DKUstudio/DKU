
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class Q_WaitForLoginRes_Handler
{
    public static void Method(Packet packet)
    {
        Q_WaitForLoginRes res = Data<Q_WaitForLoginRes>.Deserialize(packet.m_data);

    }
}
