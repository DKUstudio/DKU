
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Packets.var
{
    public class C_StopWaitingReq_Handler
    {
        public static void Method(Packet packet)
        {
            C_StopWaitingReq req = Data<C_StopWaitingReq>.Deserialize(packet.m_data);
            if(NetworkManager.Instance.m_waiting_list.ContainsKey(req.waiting_id))
            {
                NetworkManager.Instance.m_waiting_list.Remove(req.waiting_id);
                NetworkManager.Instance.ReturnWaitingId(req.waiting_id);
            }
        }
    }
}
