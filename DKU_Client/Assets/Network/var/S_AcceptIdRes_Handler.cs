
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class S_AcceptIdRes_Handler
{
    public static void Method(Packet packet)
    {
        S_AcceptIdRes res = Data<S_AcceptIdRes>.Deserialize(packet.m_data);
        //TODO
    }
}
