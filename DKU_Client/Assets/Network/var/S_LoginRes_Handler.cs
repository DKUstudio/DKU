
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
            Debug.Log("[S_LoginRes_Handler] Login Success");
            NetworkManager.Instance.Connections.is_waiting = false;
            NetworkManager.Instance.Connections.logged_in = true;
            NetworkManager.Instance.Connections.udata = res.udata;
        }
        else
        {
            Debug.Log("[S_LoginRes_Handler] Login fail...");
        }
    }
}
