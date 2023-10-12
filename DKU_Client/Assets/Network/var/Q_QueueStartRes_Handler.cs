
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class Q_QueueStartRes_Handler
{
    public static void Method(Packet packet)
    {
        Q_QueueStartRes res = Data<Q_QueueStartRes>.Deserialize(packet.m_data);

    }
}
