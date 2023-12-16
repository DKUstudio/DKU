
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKU_Server.Connections;
using DKU_ServerCore.Packets.var.server;

namespace DKU_Server.Packets.var
{
    public class C_UserDataReq_Handler
    {
        public static void Method(SPacket packet)
        {
            C_UserDataReq req = Data<C_UserDataReq>.Deserialize(packet.m_data);
            if(NetworkManager.Instance.world.uid_users.ContainsKey(req.uid))
            {
                S_UserDataRes res = new S_UserDataRes();
                res.udata = NetworkManager.Instance.world.uid_users[req.uid].udata;
                byte[] body = res.Serialize();
                Packet pkt = new Packet(PacketType.S_UserDataRes, body, body.Length);
                packet.USER_TOKEN.Send(pkt);
            }
        }
    }
}
