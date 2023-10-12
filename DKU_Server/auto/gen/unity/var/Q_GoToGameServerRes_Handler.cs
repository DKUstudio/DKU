
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class Q_GoToGameServerRes_Handler
{
    public static void Method(Packet packet)
    {
        Q_GoToGameServerRes res = Data<Q_GoToGameServerRes>.Deserialize(packet.m_data);

    }
}
