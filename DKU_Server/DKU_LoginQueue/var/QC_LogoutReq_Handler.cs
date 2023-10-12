
using DKU_ServerCore.Packets.var.gserver;
using DKU_ServerCore.Packets.var.qclient;
using DKU_ServerCore.Packets;

public class QC_LogoutReq_Handler
{
    public static void Method(Packet packet)
    {
        QC_LogoutReq res = Data<QC_LogoutReq>.Deserialize(packet.m_data);

    }
}
