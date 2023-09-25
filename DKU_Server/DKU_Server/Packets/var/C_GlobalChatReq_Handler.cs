
using DKU_Server.Worlds;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Packets.var
{
    public class C_GlobalChatReq_Handler
    {
        public static void Method(Packet packet)
        {
            C_GlobalChatReq req = Data<C_GlobalChatReq>.Deserialize(packet.m_data);
            TheWorld.Instance.GlobalChat(req);
        }
    }
}
