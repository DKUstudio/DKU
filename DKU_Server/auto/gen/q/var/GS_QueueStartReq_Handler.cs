
using DKU_ServerCore.Packets.var.gserver;
using DKU_ServerCore.Packets.var.qclient;
using DKU_ServerCore.Packets;

public class GS_QueueStartReq_Handler
{
    public static void Method(Packet packet)
    {
        GS_QueueStartReq res = Data<GS_QueueStartReq>.Deserialize(packet.m_data);

    }
}
