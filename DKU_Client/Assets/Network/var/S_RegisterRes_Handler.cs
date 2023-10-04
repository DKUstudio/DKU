using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_RegisterRes_Handler
{
    public static void Method(Packet packet)
    {
        S_RegisterRes res = Data<S_RegisterRes>.Deserialize(packet.m_data);
        if (res.success)
        {
            Debug.Log("[S_RegisterRes_Handler] Register <color=green>Success</color>");
        }
        else
        {
            Debug.Log("[S_RegisterRes_Handler] Register <color=red>fail</color>...");
        }
    }
}
