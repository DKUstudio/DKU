
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_UserCharaDataRes_Handler
{
    public static void Method(Packet packet)
    {
        S_UserCharaDataRes res = Data<S_UserCharaDataRes>.Deserialize(packet.m_data);

    }
}
