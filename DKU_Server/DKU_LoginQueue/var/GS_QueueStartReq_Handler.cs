
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using UnityEngine;

public class GS_QueueStartReq_Handler
{
    public static void Method(Packet packet)
    {
        GS_QueueStartReq res = Data<GS_QueueStartReq>.Deserialize(packet.m_data);

    }
}
