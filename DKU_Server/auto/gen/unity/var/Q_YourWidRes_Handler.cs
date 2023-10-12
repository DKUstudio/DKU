
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class Q_YourWidRes_Handler
{
    public static void Method(Packet packet)
    {
        Q_YourWidRes res = Data<Q_YourWidRes>.Deserialize(packet.m_data);

    }
}
