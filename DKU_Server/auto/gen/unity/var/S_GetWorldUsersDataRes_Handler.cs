
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_GetWorldUsersDataRes_Handler
{
    public static void Method(Packet packet)
    {
        S_GetWorldUsersDataRes res = Data<S_GetWorldUsersDataRes>.Deserialize(packet.m_data);

    }
}
