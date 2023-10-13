
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using UnityEngine;

// 이메일을 성공적으로 송신했는지 여부
public class S_StartAuthRes_Handler
{
    public static void Method(Packet packet)
    {
        S_StartAuthRes res = Data<S_StartAuthRes>.Deserialize(packet.m_data);
        System.Console.WriteLine("[S_StartAuthRes_Handler] " + res.success);
    }
}
