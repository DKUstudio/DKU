
using DKU_ServerCore.Packets.var.gserver;
using DKU_ServerCore.Packets.var.qclient;
using DKU_ServerCore.Packets;

public class QC_LoginReq_Handler
{
    public static void Method(Packet packet)
    {
        QC_LoginReq res = Data<QC_LoginReq>.Deserialize(packet.m_data);

    }
}
