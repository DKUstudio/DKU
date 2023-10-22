using System.Collections;
using System.Collections.Generic;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using Sirenix.OdinInspector;
using UnityEngine;

public class ChatService : MonoBehaviour
{
    // [ShowInInspector]
    // public static void GlobalChat(string message)
    // {
    //     if (NetworkManager.Instance.Connections.logged_in == false)
    //         return;
    //     C_GlobalChatReq req = new C_GlobalChatReq();
    //     // TODO
    //     // req.chat_message = message;
    //     // req.udata = NetworkManager.Instance.Connections.udata;
    //     byte[] body = req.Serialize();

    //     // Packet packet = new Packet(PacketType.C_GlobalChatReq, body, body.Length);
    //     // NetworkManager.Instance.Connections.Send(packet);
    // }

    [ShowInInspector]
    public static void Chat(ChatData data)
    {
        C_ChatReq req = new C_ChatReq();
        req.chatData = data;
        req.chatData.sender_uid = NetworkManager.Instance.UDATA.uid;
        byte[] body = req.Serialize();

        Packet packet = new Packet(PacketType.C_ChatReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(packet);
    }
}
