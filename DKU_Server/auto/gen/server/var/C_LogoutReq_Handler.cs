
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKU_Server.Connections;

namespace DKU_Server.Packets.var
{
    public class C_LogoutReq_Handler
    {
        public static void Method(SPacket packet)
        {
            C_LogoutReq req = Data<C_LogoutReq>.Deserialize(packet.m_data);

        }
    }
}
