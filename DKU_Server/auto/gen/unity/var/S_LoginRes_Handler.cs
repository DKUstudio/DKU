
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class S_LoginRes_Handler
{
    public static void Method(Packet packet)
    {
        S_LoginRes res = Data<S_LoginRes>.Deserialize(packet.m_data);
        //TODO
    }
}
