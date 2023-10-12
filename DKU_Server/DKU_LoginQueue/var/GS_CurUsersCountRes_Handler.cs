
using DKU_ServerCore.Packets.var.gserver;
using DKU_ServerCore.Packets.var.qclient;
using DKU_ServerCore.Packets;
using DKU_LoginQueue;

public class GS_CurUsersCountRes_Handler
{
    public static void Method(Packet packet)
    {
        GS_CurUsersCountRes res = Data<GS_CurUsersCountRes>.Deserialize(packet.m_data);
        NetworkManager.Instance.LoginUsers(res.cur_login_users_count);

        Console.WriteLine($"[GameServer] available seats: {res.cur_login_users_count}");
    }
}
