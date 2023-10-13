
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 핑 체크 반환
// from C_PingReq
public class S_PingRes_Handler
{
    public static void Method(Packet packet)
    {
        S_PingRes res = Data<S_PingRes>.Deserialize(packet.m_data);
        //TODO
    }
}
