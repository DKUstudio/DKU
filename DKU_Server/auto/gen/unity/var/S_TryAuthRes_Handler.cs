
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_TryAuthRes_Handler
{
    public static void Method(Packet packet)
    {
        S_TryAuthRes res = Data<S_TryAuthRes>.Deserialize(packet.m_data);

    }
}
