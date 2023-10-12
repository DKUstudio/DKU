
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;
using DKU_ServerCore.Packets.var.client;

public class S_YourSidRes_Handler
{
    public static void Method(Packet packet)
    {
        S_YourSidRes res = Data<S_YourSidRes>.Deserialize(packet.m_data);
        Debug.Log($"[GameServer] sid confirmed: {res.sid}");

        C_MyUserDataReq req = new C_MyUserDataReq();
        req.sid = res.sid;
        req.udata = NetworkManager.Instance.UDATA;
        byte[] body = req.Serialize();

        Packet pkt = new Packet(PacketType.C_MyUserDataReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(pkt);
    }
}
