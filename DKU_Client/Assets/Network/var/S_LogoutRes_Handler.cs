
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 로그아웃 성공 여부
// from C_LogoutReq
public class S_LogoutRes_Handler
{
    public static void Method(Packet packet)
    {
        S_LogoutRes res = Data<S_LogoutRes>.Deserialize(packet.m_data);
        //TODO
    }
}
