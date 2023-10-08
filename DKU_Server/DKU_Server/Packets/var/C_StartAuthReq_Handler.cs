
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKU_Server.DBs;
using DKU_ServerCore.Packets.var.server;
using DKU_Server.Connections.Tokens;
using DKU_Server.Worlds;

namespace DKU_Server.Packets.var
{
    public class C_StartAuthReq_Handler
    {
        public static void Method(Packet packet)
        {
            C_StartAuthReq req = Data<C_StartAuthReq>.Deserialize(packet.m_data);

            bool success = AuthManager.Instance.StartAuth(req.uid, req.email);
            S_StartAuthRes res = new S_StartAuthRes();
            res.success = success;
            byte[] body = res.Serialize();

            Packet packet1 = new Packet(PacketType.S_StartAuthRes, body, body.Length);
            UserToken token = TheWorld.Instance.FindUserToken(req.uid);
            if(token != null)
                token.Send(packet1);
        }
    }
}
