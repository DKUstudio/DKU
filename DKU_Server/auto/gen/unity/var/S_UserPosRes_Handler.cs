
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_UserPosRes_Handler
{
    public static void Method(Packet packet)
    {
        S_UserPosRes res = Data<S_UserPosRes>.Deserialize(packet.m_data);

    }
}
