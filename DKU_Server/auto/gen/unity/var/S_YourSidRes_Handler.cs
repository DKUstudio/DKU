
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_YourSidRes_Handler
{
    public static void Method(Packet packet)
    {
        S_YourSidRes res = Data<S_YourSidRes>.Deserialize(packet.m_data);

    }
}
