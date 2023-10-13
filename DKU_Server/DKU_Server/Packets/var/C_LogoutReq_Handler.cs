
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
            if (NetworkManager.Instance.world.FindUserToken(req.uid) == null)  // sid
            {
                NetworkManager.Instance.world.RemoveSidUser(req.sid);
            }
            else
            {
                NetworkManager.Instance.world.RemoveUidUser(req.uid);
            }
        }
    }
}
