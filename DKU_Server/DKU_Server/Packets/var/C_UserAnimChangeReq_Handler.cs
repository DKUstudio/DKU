
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
    public class C_UserAnimChangeReq_Handler
    {
        public static void Method(SPacket packet)
        {
            C_UserAnimChangeReq req = Data<C_UserAnimChangeReq>.Deserialize(packet.m_data);
            NetworkManager.Instance.world.ShootLocalAnimChanges(req.uid, req.animName);
        }
    }
}
