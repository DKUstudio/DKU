
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class Q_LoginRes_Handler
{
    public static void Method(Packet packet)
    {
        Q_LoginRes res = Data<Q_LoginRes>.Deserialize(packet.m_data);
        if (res.success)
        {
            Debug.Log("[Login] <color=green>success</color>");
        }
        else
        {
            Debug.Log("[Login] <color=red>fail</color>");
        }
    }
}
