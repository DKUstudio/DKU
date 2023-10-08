
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// from C_WaitingIdReq
// 로그인 전 대기열 번호
public class S_WaitingIdRes_Handler
{
    public static void Method(Packet packet)
    {
        S_WaitingIdRes res = Data<S_WaitingIdRes>.Deserialize(packet.m_data);
        NetworkManager.Instance.Connections.SetWaiting(true, res.waiting_id);
    }
}
