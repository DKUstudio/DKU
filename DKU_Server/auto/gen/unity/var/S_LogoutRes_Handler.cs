
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_LogoutRes_Handler
{
    public static void Method(Packet packet)
    {
        S_LogoutRes res = Data<S_LogoutRes>.Deserialize(packet.m_data);

    }
}
