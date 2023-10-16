
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Packets.var
{
    public class Q_YourWidRes_Handler
    {
        public static void Method(SPacket packet)
        {
            Q_YourWidRes req = Data<Q_YourWidRes>.Deserialize(packet.m_data);

        }
    }
}
