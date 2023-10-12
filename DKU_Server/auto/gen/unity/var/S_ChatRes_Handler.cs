
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_ChatRes_Handler
{
    public static void Method(Packet packet)
    {
        S_ChatRes res = Data<S_ChatRes>.Deserialize(packet.m_data);

    }
}
