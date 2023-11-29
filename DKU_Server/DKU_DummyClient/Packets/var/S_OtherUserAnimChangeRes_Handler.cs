
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClient.Packets.var
{
    public class S_OtherUserAnimChangeRes_Handler
    {
        public static void Method(Packet packet)
        {
            S_OtherUserAnimChangeRes res = Data<S_OtherUserAnimChangeRes>.Deserialize(packet.m_data);

        }
    }
}
