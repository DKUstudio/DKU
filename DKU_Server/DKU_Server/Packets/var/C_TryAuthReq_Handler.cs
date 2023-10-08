
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKU_Server.DBs;

namespace DKU_Server.Packets.var
{
    public class C_TryAuthReq_Handler
    {
        public static void Method(Packet packet)
        {
            C_TryAuthReq req = Data<C_TryAuthReq>.Deserialize(packet.m_data);
            AuthManager.Instance.VerifyEmailCode(req.uid, req.code);
        }
    }
}
