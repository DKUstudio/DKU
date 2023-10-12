using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using UnityEngine;

// from C_RegisterReq
// success : 회원가입 성공/ 실패 여부
public class S_RegisterRes_Handler
{
    public static void Method(Packet packet)
    {
        S_RegisterRes res = Data<S_RegisterRes>.Deserialize(packet.m_data);
        if (res.success)
        {
            Debug.Log("[S_RegisterRes_Handler] Register <color=green>Success</color>");
            login_input.instance.loadingcanvas.SetActive(false);
            Debug.Log(login_input.instance.Signin_NAME.text);
        }
        else
        {
            Debug.Log("[S_RegisterRes_Handler] Register <color=red>fail</color>...");
            login_input.instance.loadingcanvas.SetActive(false);
            login_input.instance.registfail();
        }
    }
}
