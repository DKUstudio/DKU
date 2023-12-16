
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_UserDataRes_Handler
{
    public static void Method(Packet packet)
    {
        S_UserDataRes res = Data<S_UserDataRes>.Deserialize(packet.m_data);

    }
}
