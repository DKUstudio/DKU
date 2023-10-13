
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

// 로그인 서버 고유 id 수신
public class Q_YourWidRes_Handler
{
    public static void Method(Packet packet)
    {
        Q_YourWidRes res = Data<Q_YourWidRes>.Deserialize(packet.m_data);
        NetworkManager.Instance.Connections.SetWaiting(true, res.wid);
    }
}
