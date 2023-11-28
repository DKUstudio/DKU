
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_OtherUserCharShiftChangedRes_Handler
{
    public static void Method(Packet packet)
    {
        S_OtherUserCharShiftChangedRes res = Data<S_OtherUserCharShiftChangedRes>.Deserialize(packet.m_data);
        GameManager.Instance.WorldManager?.OtherPlayerManager?.CharaShiftChange(res.uid, res.shift);
    }
}
