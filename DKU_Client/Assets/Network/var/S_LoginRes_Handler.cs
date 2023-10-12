
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            login_input.instance.loadingcanvas.SetActive(false);
            SceneManager.LoadScene("MainMap");
        }
        else
        {
            Debug.Log("[S_LoginRes_Handler] Login <color=red>fail</color>...");
            login_input.instance.loadingcanvas.SetActive(false);
            login_input.instance.loginfail();
        }
    }
}
