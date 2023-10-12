
using DKU_ServerCore.Packets.var.gserver;
using DKU_ServerCore.Packets.var.qclient;
using DKU_ServerCore.Packets;
using DKU_LoginQueue;
using DKU_ServerCore.Packets.var.queue;

public class QC_RegisterReq_Handler
{
    public static void Method(Packet packet)
    {
        QC_RegisterReq req = Data<QC_RegisterReq>.Deserialize(packet.m_data);
        bool isTrue = NetworkManager.Instance.m_wid_list.TryGetValue(req.wid, out UserToken token);
        if (isTrue == false)
            return;

        bool success = NetworkManager.Instance.m_database_manager.Register(req.id, req.salt, req.pw, req.nickname);

        Q_RegisterRes res = new Q_RegisterRes();
        res.success = success;
        byte[] serial = res.Serialize();

        Packet pkt = new Packet();
        pkt.SetData(PacketType.Q_RegisterRes, serial, serial.Length);
        token.Send(pkt);
    }
}
