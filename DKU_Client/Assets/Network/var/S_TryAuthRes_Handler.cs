
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using UnityEngine;

// 이메일 코드 인증이 성공했는지 여부
public class S_TryAuthRes_Handler
{
    public static void Method(Packet packet)
    {
        S_TryAuthRes res = Data<S_TryAuthRes>.Deserialize(packet.m_data);
        System.Console.WriteLine("[S_TryAuthRes_Handler] " + res.success.ToString());
    }
}
