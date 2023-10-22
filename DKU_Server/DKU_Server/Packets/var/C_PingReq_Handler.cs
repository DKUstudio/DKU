
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKU_Server.Worlds;
using DKU_ServerCore.Packets.var.server;
using DKU_Server.Connections;

namespace DKU_Server.Packets.var
{
    public class C_PingReq_Handler
    {
        public static void Method(SPacket packet)
        {
            C_PingReq req = Data<C_PingReq>.Deserialize(packet.m_data);

            S_PingRes res = new S_PingRes();
            res.send_ms = req.send_ms;
            byte[] serial = res.Serialize();

            Packet pkt = new Packet(PacketType.S_PingRes, serial, serial.Length);
            NetworkManager.Instance.world.uid_users.TryGetValue(req.uid, out var user);
            if (user != null)
            {
                user.Send(pkt);
            }
        }
    }
}
