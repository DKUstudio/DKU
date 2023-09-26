using System.Collections;
using System.Collections.Generic;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using Sirenix.OdinInspector;
using UnityEngine;

public class ChatService : MonoBehaviour
{
    [ShowInInspector]
    public static void GlobalChat(string message)
    {
        C_GlobalChatReq req = new C_GlobalChatReq();
        req.chat_message = message;
        req.udata = NetworkManager.Instance.Connections.udata;
        byte[] body = req.Serialize();

        Packet packet = new Packet(PacketType.C_GlobalChatReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(packet);
    }
}
