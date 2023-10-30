
using DKU_Server.Connections.Tokens;
using DKU_Server.Connections;
using DKU_Server.Worlds;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Packets.var
{
    public class C_LoginReq_Handler
    {
        public static void Method(SPacket packet)
        {
            // 로그인 성공시, 대기 리스트에서 월드방으로 이동
            C_LoginReq req = Data<C_LoginReq>.Deserialize(packet.m_data);
        }
    }
}
