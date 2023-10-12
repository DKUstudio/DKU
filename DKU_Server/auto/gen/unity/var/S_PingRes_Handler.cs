
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_PingRes_Handler
{
    public static void Method(Packet packet)
    {
        S_PingRes res = Data<S_PingRes>.Deserialize(packet.m_data);

    }
}
