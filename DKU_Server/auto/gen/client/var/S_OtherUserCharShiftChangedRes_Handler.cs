
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClient.Packets.var
{
    public class S_OtherUserCharShiftChangedRes_Handler
    {
        public static void Method(Packet packet)
        {
            S_OtherUserCharShiftChangedRes res = Data<S_OtherUserCharShiftChangedRes>.Deserialize(packet.m_data);

        }
    }
}
