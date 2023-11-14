
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_OXGameStartRes_Handler
{
    public static void Method(Packet packet)
    {
        S_OXGameStartRes res = Data<S_OXGameStartRes>.Deserialize(packet.m_data);

    }
}
