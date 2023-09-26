
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

public class S_GlobalChatRes_Handler
{
    public static void Method(Packet packet)
    {
        UnityEngine.Debug.Log("chat");

        S_GlobalChatRes res = Data<S_GlobalChatRes>.Deserialize(packet.m_data);
        //TODO
        UnityEngine.Debug.Log(res.chat_message);
    }
}
