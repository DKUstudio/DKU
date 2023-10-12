
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
        NetworkManager.Instance.Connections.CloseSocketConnection();

        Task t = new Task(() => CheckDisconnected(res.game_server_ipv4));
        t.Start();
    }

    static void CheckDisconnected(string ip_address)
    {
        while (NetworkManager.Instance.Connections.M_socket != null)
        {
            Thread.Sleep(1000);
        }
        NetworkManager.Instance.Connections.Connect(ip_address);
        Debug.Log("[GameServer] enter confirmed");
    }
}
