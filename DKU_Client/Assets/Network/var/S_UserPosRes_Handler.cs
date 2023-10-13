
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using UnityEngine;

// 유저 위치 정보 수신 패킷
// from C_UserPosReq
public class S_UserPosRes_Handler
{
    public static void Method(Packet packet)
    {
        S_UserPosRes res = Data<S_UserPosRes>.Deserialize(packet.m_data);

    }
}
