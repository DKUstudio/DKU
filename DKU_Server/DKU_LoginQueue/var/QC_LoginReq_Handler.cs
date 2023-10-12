
using DKU_ServerCore.Packets.var.gserver;
using DKU_ServerCore.Packets.var.qclient;
using DKU_ServerCore.Packets;
using DKU_LoginQueue;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore;

public class QC_LoginReq_Handler
{
    public static void Method(Packet packet)
    {
        QC_LoginReq req = Data<QC_LoginReq>.Deserialize(packet.m_data);

        bool isTrue = NetworkManager.Instance.m_wid_list.TryGetValue(req.wid, out UserToken token);
        if (isTrue == false)
        {
            Console.WriteLine("[LoginReq] Not in waiting_list");
            return;
        }

        UserData udata = NetworkManager.Instance.m_database_manager.Login(req.id, req.pw);
        Q_LoginRes res = new Q_LoginRes();
        if (udata != null)
        {
            // 성공
            res.success = true;
            res.udata = udata;
            res.game_server_ipv4 = CommonDefine.IPv4_ADDRESS;

            NetworkManager.Instance.Returnwid(req.wid);
            Console.WriteLine("[LoginReq] login success");
        }
        else
        {
            // 실패
            res.success = false;
        }
        byte[] serial = res.Serialize();

        Packet pkt = new Packet(PacketType.Q_LoginRes, serial, serial.Length);
        token.Send(pkt);
    }
}
