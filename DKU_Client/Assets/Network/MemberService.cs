using System.Collections;
using System.Collections.Generic;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.qclient;
using Sirenix.OdinInspector;
using UnityEngine;

public class MemberService : MonoBehaviour
{
    /// <summary>
    /// ȸ������ �õ�
    /// </summary>
    /// <param name="id"></param>
    /// <param name="pw"></param>
    /// <param name="nickname"></param>
    [ShowInInspector]
    public static void Register_Request(string id, string pw, string nickname)
    {
        Crypto.SHA256_Generate(pw);
        //Debug.Log(Crypto.salt + " / " + Crypto.hashed_password);

        QC_RegisterReq req = new QC_RegisterReq();
        req.wid = NetworkManager.Instance.WID;
        req.id = id;
        req.salt = Crypto.salt;
        req.pw = Crypto.hashed_password;
        req.nickname = nickname;
        byte[] body = req.Serialize();

        Packet packet = new Packet(PacketType.QC_RegisterReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(packet);
    } // S_RegisterRes.cs
    /// <summary>
    /// �α��� �õ�
    /// </summary>
    /// <param name="id"></param>
    /// <param name="pw"></param>
    [ShowInInspector]
    public static void Login_Request(string id, string pw)
    {
        if (NetworkManager.Instance.IS_LOGGED_IN == true)
        {
            Debug.Log("[Login_Request] Already Logged in...");
            return;
        }
        QC_LoginReq req = new QC_LoginReq();
        req.wid = NetworkManager.Instance.WID;
        req.id = id;
        req.pw = pw;
        byte[] body = req.Serialize();

        Packet packet = new Packet(PacketType.QC_LoginReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(packet);
    }   // S_LoginRes.cs
    /// <summary>
    /// �α׾ƿ� �뺸
    /// </summary>
    [ShowInInspector]
    public static void Logout_Request()
    {
        if (NetworkManager.Instance.IS_LOGGED_IN == false)
        {
            Debug.Log("[Logout_Request] Not Logged in...");
            return;
        }
        C_LogoutReq req = new C_LogoutReq();
        req.uid = NetworkManager.Instance.UDATA.uid;
        byte[] body = req.Serialize();

        Packet packet = new Packet(PacketType.C_LogoutReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(packet);

        NetworkManager.Instance.Connections.SetLogin(false, null);
    }   // S_LogoutRes.cs

    [ShowInInspector]
    public static void StartAuth_Request(string email)
    {
        if (NetworkManager.Instance.IS_LOGGED_IN == false)
        {
            Debug.Log("[Logout_Request] Not Logged in...");
            return;
        }
        C_StartAuthReq req = new C_StartAuthReq();
        req.uid = NetworkManager.Instance.UDATA.uid;
        req.email = email;
        byte[] body = req.Serialize();

        Packet packet = new Packet(PacketType.C_StartAuthReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(packet);
    }

    [ShowInInspector]
    public static void TryAuth_Request(string code)
    {
        if (NetworkManager.Instance.IS_LOGGED_IN == false)
        {
            Debug.Log("[Logout_Request] Not Logged in...");
            return;
        }
        C_TryAuthReq req = new C_TryAuthReq();
        req.uid = NetworkManager.Instance.UDATA.uid;
        req.code = code;
        byte[] body = req.Serialize();

        Packet packet = new Packet(PacketType.C_TryAuthReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(packet);
    }

    /// <summary>
    /// ���� �𵨸� ������ ������ �α��� ������ ���ÿ� ������ ��
    /// </summary>
    /// <param name="v_bitmask"></param>
    /// <param name="v_lastloginshift"></param>
    public static void CharaDataChanged(int v_bitmask, short v_lastloginshift)
    {
        if (NetworkManager.Instance.UDATA == null)
        {
            Debug.Log("UDATA ���� x");
            return;
        }
        C_UserCharaDataChangeReq req = new C_UserCharaDataChangeReq();
        req.uid = NetworkManager.Instance.UDATA.uid;
        req.changed_bitmask = v_bitmask;
        req.changed_lastloginshift = v_lastloginshift;
        byte[] body = req.Serialize();
        Packet pkt = new Packet(PacketType.C_UserCharaDataChangeReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(pkt);
    }
    // �𵨸� ������ ������ ��
    /// <summary>
    /// ���� �𵨸� ������ ������ ��
    /// </summary>
    /// <param name="v_bitmask"></param>
    public static void CharaDataBitmaskChanged(int v_bitmask)
    {
        if (NetworkManager.Instance.UDATA == null)
        {
            Debug.Log("UDATA ���� x");
            return;
        }
        C_UserCharaDataBitChangeReq req = new C_UserCharaDataBitChangeReq();
        req.uid = NetworkManager.Instance.UDATA.uid;
        req.changed_bitmask = v_bitmask;
        byte[] body = req.Serialize();
        Packet pkt = new Packet(PacketType.C_UserCharaDataChangeReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(pkt);
    }
    /// <summary>
    /// ������ �α��� ������ ������ ��
    /// </summary>
    /// <param name="v_shift"></param>
    public static void CharaDataShiftChanged(short v_lastloginshift)
    {
        if (NetworkManager.Instance.UDATA == null)
        {
            Debug.Log("UDATA ���� x");
            return;
        }
        C_UserCharaDataShiftChangeReq req = new C_UserCharaDataShiftChangeReq();
        req.uid = NetworkManager.Instance.UDATA.uid;
        req.changed_lastloginshift = v_lastloginshift;
        byte[] body = req.Serialize();
        Packet pkt = new Packet(PacketType.C_UserCharaDataChangeReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(pkt);
        Debug.Log($"[Chara change] {v_lastloginshift}");
    }

    /// <summary>
    /// �������� �����ϰ� ���ھ� �������� ����
    /// </summary>
    /// <param name="score"></param>
    public static void SuikaGameResult(long score)
    {
        if (NetworkManager.Instance.UDATA == null)
            return;
        C_SuikaGameResultReq req = new C_SuikaGameResultReq();
        req.uid = NetworkManager.Instance.UDATA.uid;
        req.score = score;
        byte[] body = req.Serialize();
        Packet pkt = new Packet(PacketType.C_SuikaGameResultReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(pkt);
    }
}
