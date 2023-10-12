using System.Collections;
using System.Collections.Generic;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.qclient;
using Sirenix.OdinInspector;
using UnityEngine;

public class MemberService : MonoBehaviour
{
    [ShowInInspector]
    public static void Register_Request(string id, string pw, string nickname)
    {
        Crypto.SHA256_Generate(pw);
        Debug.Log(Crypto.salt + " / " + Crypto.hashed_password);

        QC_RegisterReq req = new QC_RegisterReq();
        req.wid = NetworkManager.Instance.Connections.waiting_id;
        req.id = id;
        req.salt = Crypto.salt;
        req.pw = Crypto.hashed_password;
        req.nickname = nickname;
        byte[] body = req.Serialize();

        Packet packet = new Packet(PacketType.QC_RegisterReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(packet);
    } // S_RegisterRes.cs

    [ShowInInspector]
    public static void Login_Request(string id, string pw)
    {
        if (NetworkManager.Instance.Connections.logged_in == true)
        {
            Debug.Log("[Login_Request] Already Logged in...");
            return;
        }
        QC_LoginReq req = new QC_LoginReq();
        req.wid = NetworkManager.Instance.Connections.waiting_id;
        req.id = id;
        req.pw = pw;
        byte[] body = req.Serialize();

        Packet packet = new Packet(PacketType.QC_LoginReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(packet);
    }   // S_LoginRes.cs

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

        NetworkManager.Instance.Connections.SetLogin(false, null);
    }   // S_LogoutRes.cs

    [ShowInInspector]
    public static void StartAuth_Request(string email)
    {
        if (NetworkManager.Instance.Connections.logged_in == false)
        {
            Debug.Log("[Logout_Request] Not Logged in...");
            return;
        }
        C_StartAuthReq req = new C_StartAuthReq();
        req.uid = NetworkManager.Instance.Connections.udata.uid;
        req.email = email;
        byte[] body = req.Serialize();

        Packet packet = new Packet(PacketType.C_StartAuthReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(packet);
    }

    [ShowInInspector]
    public static void TryAuth_Request(string code)
    {
        if (NetworkManager.Instance.Connections.logged_in == false)
        {
            Debug.Log("[Logout_Request] Not Logged in...");
            return;
        }
        C_TryAuthReq req = new C_TryAuthReq();
        req.uid = NetworkManager.Instance.Connections.udata.uid;
        req.code = code;
        byte[] body = req.Serialize();

        Packet packet = new Packet(PacketType.C_TryAuthReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(packet);
    }
}
