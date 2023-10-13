
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

// 남은 대기열 수 보내주는 패킷
// 나보다 앞에 있는 대기자 수를 보내줌
public class Q_WaitForLoginRes_Handler
{
    public static void Method(Packet packet)
    {
        Q_WaitForLoginRes res = Data<Q_WaitForLoginRes>.Deserialize(packet.m_data);

    }
}
