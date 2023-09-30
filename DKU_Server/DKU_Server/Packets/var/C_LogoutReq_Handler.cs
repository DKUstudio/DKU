
using DKU_Server.Connections.Tokens;
using DKU_Server.Worlds;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Packets.var
{
    public class C_LogoutReq_Handler
    {
        public static void Method(Packet packet)
        {
            C_LogoutReq req = Data<C_LogoutReq>.Deserialize(packet.m_data);

            TheWorld.Instance.users.TryGetValue(req.uid, out var user);
            if (user == null)   // no such data
                return;
            TheWorld.Instance.LogoutUser(req.uid);

            // resend waiting id
            long waiting_id = NetworkManager.Instance.GetWaitingId();
            NetworkManager.Instance.m_waiting_list.Add(waiting_id, user.UserToken);
            S_WaitingIdRes res = new S_WaitingIdRes();
            res.waiting_id = waiting_id;
            byte[] body = res.Serialize();

            Packet pkt = new Packet(PacketType.S_WaitingIdRes, body, body.Length);
            user.UserToken.Send(pkt);
        }
    }
}
