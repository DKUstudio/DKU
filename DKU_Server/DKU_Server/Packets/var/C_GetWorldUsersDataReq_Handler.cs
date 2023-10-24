
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
using System.Runtime.InteropServices;

namespace DKU_Server.Packets.var
{
    public class C_GetWorldUsersDataReq_Handler
    {
        public static void Method(SPacket packet)
        {
            C_GetWorldUsersDataReq req = Data<C_GetWorldUsersDataReq>.Deserialize(packet.m_data);

            List<UserData> users = NetworkManager.Instance.world.GetCurWorldUserDatas(req.uid);

            S_GetWorldUsersDataRes res = new S_GetWorldUsersDataRes();
            res.ulist = users;
            byte[] body = res.Serialize();

            Packet pkt = new Packet(PacketType.S_GetWorldUsersDataRes, body, body.Length);
            packet.USER_TOKEN.Send(pkt);
        }
    }
}
