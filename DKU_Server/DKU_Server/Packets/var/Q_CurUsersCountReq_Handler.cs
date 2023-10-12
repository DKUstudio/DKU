
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKU_ServerCore.Packets.var.gserver;
using DKU_ServerCore;

namespace DKU_Server.Packets.var
{
    public class Q_CurUsersCountReq_Handler
    {
        public static void Method(Packet packet)
        {
            Q_CurUsersCountReq req = Data<Q_CurUsersCountReq>.Deserialize(packet.m_data);

            int remained = CommonDefine.MAX_CONNECTION - NetworkManager.Instance.world.users_count;
            GS_CurUsersCountRes res = new GS_CurUsersCountRes();
            res.cur_login_users_count = remained;

            Console.WriteLine($"[GameServer] available seats: {remained}");
            byte[] body = res.Serialize();

            Packet pkt = new Packet(PacketType.GS_CurUsersCountRes, body, body.Length);
            NetworkManager.Instance.m_login_queue_connector.m_token.Send(pkt);
        }
    }
}
