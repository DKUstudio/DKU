
using DKU_ServerCore.Packets.var.gserver;
using DKU_ServerCore.Packets.var.qclient;
using DKU_ServerCore.Packets;

public class GS_CurUsersCountRes_Handler
{
    public static void Method(Packet packet)
    {
        GS_CurUsersCountRes res = Data<GS_CurUsersCountRes>.Deserialize(packet.m_data);

    }
}
