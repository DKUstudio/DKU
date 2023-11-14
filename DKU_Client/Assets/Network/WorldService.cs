using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DKU_ServerCore;
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets;

public class WorldService
{
    public static void ChangeWorld(CommonDefine.WorldBlockType btype)
    {
        // 변경을 원하는 월드로 입장이 가능한지 확인하는 패킷을 송신
        if (NetworkManager.Instance.UDATA == null)
            return;
        C_WorldChangeAvailReq req = new C_WorldChangeAvailReq();
        req.udata = NetworkManager.Instance.UDATA;
        req.from_room_number = NetworkManager.Instance.current_world_block_number;
        req.room_number = (short)btype;
        byte[] body = req.Serialize();
        Packet pkt = new Packet(PacketType.C_WorldChangeAvailReq, body, body.Length);
        NetworkManager.Instance.Connections.Send(pkt);
    }
}
