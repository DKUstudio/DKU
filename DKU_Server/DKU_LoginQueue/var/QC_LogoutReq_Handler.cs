
using DKU_ServerCore.Packets.var.gserver;
using DKU_ServerCore.Packets.var.qclient;
using DKU_ServerCore.Packets;
using DKU_LoginQueue;
using DKU_ServerCore;
using System.Security.Cryptography;

public class QC_LogoutReq_Handler
{
    public static void Method(Packet packet)
    {
        QC_LogoutReq res = Data<QC_LogoutReq>.Deserialize(packet.m_data);
        NetworkManager.Instance.Returnwid(res.wid);
        LogManager.Log($"[LogoutReq] return wid {res.wid}");
    }
}
