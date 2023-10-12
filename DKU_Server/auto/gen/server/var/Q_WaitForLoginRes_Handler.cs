
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
    public class Q_WaitForLoginRes_Handler
    {
        public static void Method(Packet packet)
        {
            Q_WaitForLoginRes req = Data<Q_WaitForLoginRes>.Deserialize(packet.m_data);

        }
    }
}
