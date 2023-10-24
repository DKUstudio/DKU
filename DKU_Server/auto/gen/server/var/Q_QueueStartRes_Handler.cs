
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
    public class Q_QueueStartRes_Handler
    {
        public static void Method(SPacket packet)
        {
            Q_QueueStartRes req = Data<Q_QueueStartRes>.Deserialize(packet.m_data);

        }
    }
}
