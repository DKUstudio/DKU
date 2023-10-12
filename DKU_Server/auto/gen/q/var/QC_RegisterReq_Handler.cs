
using DKU_ServerCore.Packets.var.gserver;
using DKU_ServerCore.Packets.var.qclient;
using DKU_ServerCore.Packets;

public class QC_RegisterReq_Handler
{
    public static void Method(Packet packet)
    {
        QC_RegisterReq res = Data<QC_RegisterReq>.Deserialize(packet.m_data);

    }
}
