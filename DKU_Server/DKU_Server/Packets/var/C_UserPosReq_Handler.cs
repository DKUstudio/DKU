
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKU_Server.Connections;

namespace DKU_Server.Packets.var
{
    public class C_UserPosReq_Handler
    {
        public static void Method(SPacket packet)
        {
            C_UserPosReq req = Data<C_UserPosReq>.Deserialize(packet.m_data);
            NetworkManager.Instance.world.ShootPlayerPos(req.uid, req.pos, req.rot);
        }
    }
}
