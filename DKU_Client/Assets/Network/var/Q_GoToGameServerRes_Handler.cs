
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class Q_GoToGameServerRes_Handler
{
    public static void Method(Packet packet)
    {
        Q_GoToGameServerRes res = Data<Q_GoToGameServerRes>.Deserialize(packet.m_data);

        NetworkManager.Instance.StartConnection(res.game_server_ipv4);
    }

}
