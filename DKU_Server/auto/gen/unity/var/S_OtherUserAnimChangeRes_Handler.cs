
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_OtherUserAnimChangeRes_Handler
{
    public static void Method(Packet packet)
    {
        S_OtherUserAnimChangeRes res = Data<S_OtherUserAnimChangeRes>.Deserialize(packet.m_data);

    }
}
