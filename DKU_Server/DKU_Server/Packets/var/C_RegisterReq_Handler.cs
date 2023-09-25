
using DKU_Server.Connections.Tokens;
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
    public class C_RegisterReq_Handler
    {
        public static void Method(Packet packet)
        {
            C_RegisterReq req = Data<C_RegisterReq>.Deserialize(packet.m_data);
            bool isTrue = NetworkManager.Instance.m_waiting_list.TryGetValue(req.accept_id, out UserToken token);
            if (isTrue == false)
                return;

            bool success = NetworkManager.Instance.m_database_manager.Register(req.id, req.pw, req.nickname);

            S_RegisterRes res = new S_RegisterRes();
            res.success = success;
            byte[] serial = res.Serialize();

            Packet pkt = new Packet();
            pkt.SetData(PacketType.S_RegisterRes, serial, serial.Length);
            token.Send(pkt);
        }
    }
}
