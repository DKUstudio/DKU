
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKU_ServerCore.Packets.var.gserver;
using DKU_Server.Connections;

namespace DKU_Server.Packets.var
{
    public class Q_YourWidRes_Handler
    {
        public static void Method(SPacket packet)
        {
            Q_YourWidRes res = Data<Q_YourWidRes>.Deserialize(packet.m_data);

            GS_QueueStartReq req = new GS_QueueStartReq();
            req.wid = res.wid;
            byte[] body = req.Serialize();

            Packet pkt = new Packet(PacketType.GS_QueueStartReq, body, body.Length);
            NetworkManager.Instance.m_login_queue_connector.m_token.Send(pkt);
        }
    }
}
