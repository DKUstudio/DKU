
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using UnityEngine;

public class S_RegisterRes_Handler
{
    public static void Method(Packet packet)
    {
        S_RegisterRes res = Data<S_RegisterRes>.Deserialize(packet.m_data);
        //TODO
        UnityEngine.Debug.Log(res.success);
    }
}
