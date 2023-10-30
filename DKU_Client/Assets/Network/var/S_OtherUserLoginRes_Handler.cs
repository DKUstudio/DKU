
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_OtherUserLoginRes_Handler
{
    public static void Method(Packet packet)
    {
        S_OtherUserLoginRes res = Data<S_OtherUserLoginRes>.Deserialize(packet.m_data);
        GameManager.Instance.WorldManager.OtherPlayerManager.AddUser(res.login_uid, res.udata);
    }
}
