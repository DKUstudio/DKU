
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class Q_CurUsersCountReq_Handler
{
    public static void Method(Packet packet)
    {
        Q_CurUsersCountReq res = Data<Q_CurUsersCountReq>.Deserialize(packet.m_data);

    }
}
