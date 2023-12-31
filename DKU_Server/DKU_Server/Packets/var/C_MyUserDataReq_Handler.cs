
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKU_Server.Connections;
using DKU_ServerCore;

namespace DKU_Server.Packets.var
{
    public class C_MyUserDataReq_Handler
    {
        public static void Method(SPacket packet)
        {
            C_MyUserDataReq req = Data<C_MyUserDataReq>.Deserialize(packet.m_data);
            packet.USER_TOKEN.udata = req.udata;
            packet.USER_TOKEN.udata.charaShift = (short)NetworkManager.Instance.m_database_manager.CharaDataExists(req.udata.uid)?.lastloginshift;
            NetworkManager.Instance.world.AddNewUidUser(req.uid, packet.USER_TOKEN);
        }
    }
}
