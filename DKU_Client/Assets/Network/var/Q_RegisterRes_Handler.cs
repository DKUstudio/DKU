
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

// 회원가입 성공 여부 반환
public class Q_RegisterRes_Handler
{
    public static void Method(Packet packet)
    {
        Q_RegisterRes res = Data<Q_RegisterRes>.Deserialize(packet.m_data);
        if (res.success == 0)   // 성공
        {
            Debug.Log("[Register] <color=green>success</color>");
        }
        else if (res.success == 1)  // id 중복 실패
        {
            Debug.Log("[Register] <color=red>fail</color>...id duplicate");
            login_input.instance.loadingcanvas.SetActive(false);
            login_input.instance.registfail();
        }
        else if (res.success == 2)  // db 서버 오류
        {
            Debug.Log("[Register] <color=red>fail</color>...nickname duplicate");
            login_input.instance.loadingcanvas.SetActive(false);
            login_input.instance.registfail();
        }
    }
}
