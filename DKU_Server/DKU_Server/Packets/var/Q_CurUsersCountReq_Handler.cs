
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
using DKU_Server.Connections;
using DKU_Server.Utils;

namespace DKU_Server.Packets.var
{
    public class Q_CurUsersCountReq_Handler
    {
        public static void Method(SPacket packet)
        {
            Q_CurUsersCountReq req = Data<Q_CurUsersCountReq>.Deserialize(packet.m_data);

            //접속 가능한 명수 반환
            int remained = BufferManager.Instance.REMAINED;
            GS_CurUsersCountRes res = new GS_CurUsersCountRes();
            res.cur_login_users_count = remained;

            LogManager.Log($"[GameServer] available seats: {remained}");
            byte[] body = res.Serialize();

            Packet pkt = new Packet(PacketType.GS_CurUsersCountRes, body, body.Length);
            NetworkManager.Instance.m_login_queue_connector.m_token.Send(pkt);
        }
    }
}
