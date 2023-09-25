
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClient.Packets.var
{
    public class S_AcceptIdRes_Handler
    {
        public static void Method(Packet packet)
        {
            S_AcceptIdRes res = Data<S_AcceptIdRes>.Deserialize(packet.m_data);
            Network.Instance.m_accept_id = res.accept_id;
        }
    }
}
