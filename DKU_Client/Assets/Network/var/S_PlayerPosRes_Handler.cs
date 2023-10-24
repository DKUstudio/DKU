
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_PlayerPosRes_Handler
{
    public static void Method(Packet packet)
    {
        S_PlayerPosRes res = Data<S_PlayerPosRes>.Deserialize(packet.m_data);

        Debug.Log(res.uid.ToString()
         + new Vector3(res.pos.x, res.pos.y, res.pos.z).ToString()
         + new Vector3(res.rot.x, res.rot.y, res.rot.z).ToString());
    }
}
