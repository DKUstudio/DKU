
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_LoginRes_Handler
{
    public static void Method(Packet packet)
    {
        S_LoginRes res = Data<S_LoginRes>.Deserialize(packet.m_data);
        if (res.success)
        {
            Debug.Log("[S_LoginRes_Handler] Login <color=green>Success</color>");
            NetworkManager.Instance.Connections.SetWaiting(false, -1);
            NetworkManager.Instance.Connections.SetLogin(true, res.udata);
        }
        else
        {
            Debug.Log("[S_LoginRes_Handler] Login <color=red>fail</color>...");
        }
    }
}
