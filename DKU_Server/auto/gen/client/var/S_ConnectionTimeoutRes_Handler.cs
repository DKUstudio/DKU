
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClient.Packets.var
{
    public class S_ConnectionTimeoutRes_Handler
    {
        public static void Method(Packet packet)
        {
            S_ConnectionTimeoutRes res = Data<S_ConnectionTimeoutRes>.Deserialize(packet.m_data);

        }
    }
}
