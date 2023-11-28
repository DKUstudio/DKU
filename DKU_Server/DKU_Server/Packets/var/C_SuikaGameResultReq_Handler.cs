
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
    public class C_SuikaGameResultReq_Handler
    {
        public static void Method(SPacket packet)
        {
            C_SuikaGameResultReq req = Data<C_SuikaGameResultReq>.Deserialize(packet.m_data);

        }
    }
}
