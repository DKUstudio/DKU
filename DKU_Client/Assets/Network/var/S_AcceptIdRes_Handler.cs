
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_AcceptIdRes_Handler
{
    public static void Method(Packet packet)
    {
        S_AcceptIdRes res = Data<S_AcceptIdRes>.Deserialize(packet.m_data);
        NetworkManager.Instance.Connections.accept_id = res.accept_id;
    }
}
