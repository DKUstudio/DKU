using System.Collections;
using System.Collections.Generic;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using Sirenix.OdinInspector;
using UnityEngine;

public class MemberService : MonoBehaviour
{
    [ShowInInspector]
    public static void Register_Request(string id, string pw, string nickname)
    {
        C_RegisterReq req = new C_RegisterReq();
        req.accept_id = NetworkManager.Instance.Connections.accept_id;
        req.id = id;
        req.pw = pw;
        req.nickname = nickname;
        byte[] body = req.Serialize();

        Packet packet = new Packet(PacketType.C_RegisterReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(packet);
    }

    [ShowInInspector]
    public static void Login_Request(string id, string pw)
    {
        C_LoginReq req = new C_LoginReq();
        req.accept_id = NetworkManager.Instance.Connections.accept_id;
        req.id = id;
        req.pw = pw;
        byte[] body = req.Serialize();

        Packet packet = new Packet(PacketType.C_LoginReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(packet);
    }
}
