
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_LoginRes_Handler
{
    public static void Method(Packet packet)
    {
        S_LoginRes res = Data<S_LoginRes>.Deserialize(packet.m_data);

    }
}
