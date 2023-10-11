
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_StartAuthRes_Handler
{
    public static void Method(Packet packet)
    {
        S_StartAuthRes res = Data<S_StartAuthRes>.Deserialize(packet.m_data);

    }
}
