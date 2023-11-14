
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class S_UserCharaDataLoginRes_Handler
{
    public static void Method(Packet packet)
    {
        S_UserCharaDataLoginRes res = Data<S_UserCharaDataLoginRes>.Deserialize(packet.m_data);
        Debug.Log("[UserCharaData] 여기서 초기 캐릭터 설정해줄것" + res.bitmask.ToString() + " " + res.lastloginshift.ToString());
        
    }
}
