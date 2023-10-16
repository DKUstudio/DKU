
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

// 게임 서버 접속시키는 패킷
// 수신하면 게임 서버 연결 시도
public class Q_GoToGameServerRes_Handler
{
    public static void Method(Packet packet)
    {
        Q_GoToGameServerRes res = Data<Q_GoToGameServerRes>.Deserialize(packet.m_data);

        NetworkManager.Instance.RestartConnection(res.game_server_ipv4);
    }

}
