
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using UnityEngine;

// from C_LoginReq
// success : 로그인 성공/ 실패 여부
public class S_LoginRes_Handler
{
    public static void Method(Packet packet)
    {
        S_LoginRes res = Data<S_LoginRes>.Deserialize(packet.m_data);
        if (res.success)
        {
            Debug.Log("[S_LoginRes_Handler] Login <color=green>Success</color>");
            NetworkManager.Instance.Connections.SetWaiting(false, -1);
            NetworkManager.Instance.Connections.SetLogin(true, res.udata);
            // 페이지 이동
        }
        else
        {
            Debug.Log("[S_LoginRes_Handler] Login <color=red>fail</color>...");
        }
    }
}
