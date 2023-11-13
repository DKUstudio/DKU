
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_WorldChangeAvailRes_Handler
{
    public static void Method(Packet packet)
    {
        S_WorldChangeAvailRes res = Data<S_WorldChangeAvailRes>.Deserialize(packet.m_data);

    }
}
