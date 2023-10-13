
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_FinallyLoggedInReq_Handler
{
    public static void Method(Packet packet)
    {
        S_FinallyLoggedInReq res = Data<S_FinallyLoggedInReq>.Deserialize(packet.m_data);

    }
}
