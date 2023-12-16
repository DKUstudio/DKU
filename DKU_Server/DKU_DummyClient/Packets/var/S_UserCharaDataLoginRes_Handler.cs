
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClient.Packets.var
{
    public class S_UserCharaDataLoginRes_Handler
    {
        public static void Method(Packet packet)
        {
            S_UserCharaDataLoginRes res = Data<S_UserCharaDataLoginRes>.Deserialize(packet.m_data);

        }
    }
}
