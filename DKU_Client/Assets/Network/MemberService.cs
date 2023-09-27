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
        req.waiting_id = NetworkManager.Instance.Connections.waiting_id;
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
        if (NetworkManager.Instance.Connections.logged_in)
        {
            Debug.Log("[Login_Request] Already Logged in...");
            return;
        }
        C_LoginReq req = new C_LoginReq();
        req.accept_id = NetworkManager.Instance.Connections.waiting_id;
        req.id = id;
        req.pw = pw;
        byte[] body = req.Serialize();

        Packet packet = new Packet(PacketType.C_LoginReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(packet);
    }

    [ShowInInspector]
    public static void Logout_Request()
    {
        if (NetworkManager.Instance.Connections.logged_in == false)
        {
            Debug.Log("[Logout_Request] Not Logged in...");
            return;
        }
        C_LogoutReq req = new C_LogoutReq();
        req.uid = NetworkManager.Instance.Connections.udata.uid;
        byte[] body = req.Serialize();

        Packet packet = new Packet(PacketType.C_LogoutReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(packet);

        NetworkManager.Instance.Connections.logged_in = false;
        NetworkManager.Instance.Connections.udata = null;
    }
}
