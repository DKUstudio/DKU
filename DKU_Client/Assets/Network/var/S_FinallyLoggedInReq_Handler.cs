
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

// 완전한 로그인 성공
public class S_FinallyLoggedInReq_Handler
{
    public static void Method(Packet packet)
    {
        S_FinallyLoggedInReq res = Data<S_FinallyLoggedInReq>.Deserialize(packet.m_data);

        if (res.success == 0)
        {
            // TODO 여기에 인게임 씬 이동
            UnityEngine.Debug.Log("<color=yellow>여기서 인게임 씬 이동 하세요</color>");
        }
        else if (res.success == 1)
        {

        }
    }
}
