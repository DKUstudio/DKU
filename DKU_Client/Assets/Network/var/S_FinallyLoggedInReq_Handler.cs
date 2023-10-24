
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;
using UnityEngine.SceneManagement;

// 완전한 로그인 성공
public class S_FinallyLoggedInReq_Handler
{
    public static void Method(Packet packet)
    {
        S_FinallyLoggedInReq res = Data<S_FinallyLoggedInReq>.Deserialize(packet.m_data);

        if (res.success == 0)
        {
            // TODO 여기에 인게임 씬 이동
            UnityEngine.Debug.Log("<color=yellow>로그인 성공, 인게임 이동!</color>");
            SceneManager.LoadScene("MainMap");
        }
        else if (res.success == 1)
        {
            UnityEngine.Debug.Log("<color=yellow>로그인 성공, 인게임 이동 실패!</color>");

        }
    }
}
