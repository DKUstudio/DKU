
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Packets.var
{
    public class C_StartAuthReq_Handler
    {
        public static void Method(Packet packet)
        {
            C_StartAuthReq req = Data<C_StartAuthReq>.Deserialize(packet.m_data);

        }
    }
}
