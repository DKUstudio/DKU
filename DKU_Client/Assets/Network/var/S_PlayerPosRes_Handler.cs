
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_PlayerPosRes_Handler
{
    public static void Method(Packet packet)
    {
        S_PlayerPosRes res = Data<S_PlayerPosRes>.Deserialize(packet.m_data);
        if (!NetworkManager.Instance.IS_LOGGED_IN)
            return;
    }
}
