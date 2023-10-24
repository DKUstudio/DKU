
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;
using System;

public class S_PlayerPosRes_Handler
{
    public static void Method(Packet packet)
    {
        S_PlayerPosRes res = Data<S_PlayerPosRes>.Deserialize(packet.m_data);

        try
        {
            GameManager.Instance.WorldManager.OtherPlayerManager.ControlUserTransform(res.uid, res.pos, res.rot);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.ToString());
        }
        finally
        {
            Debug.Log(res.uid.ToString() + new Vector3(res.pos.x, res.pos.y, res.pos.z).ToString() + new Vector3(res.rot.x, res.rot.y, res.rot.z).ToString());
        }

    }
}
