
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_OtherUserLogoutRes_Handler
{
    public static void Method(Packet packet)
    {
        S_OtherUserLogoutRes res = Data<S_OtherUserLogoutRes>.Deserialize(packet.m_data);
        GameManager.Instance.WorldManager?.OtherPlayerManager?.RemoveUser(res.logout_uid);
    }
}
