
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Packets.var
{
    public class Q_QueueStartRes_Handler
    {
        public static void Method(Packet packet)
        {
            Q_QueueStartRes req = Data<Q_QueueStartRes>.Deserialize(packet.m_data);

        }
    }
}
