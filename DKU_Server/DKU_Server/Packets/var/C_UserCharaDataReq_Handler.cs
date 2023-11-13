
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKU_Server.Connections;
using DKU_Server.Variants;
using DKU_ServerCore.Packets.var.server;

namespace DKU_Server.Packets.var
{
    public class C_UserCharaDataReq_Handler
    {
        public static void Method(SPacket packet)
        {
            C_UserCharaDataReq req = Data<C_UserCharaDataReq>.Deserialize(packet.m_data);

            // 캐릭터 보유 정보 가져옴
            CharaData cdata = NetworkManager.Instance.m_database_manager.CharaDataExists(req.uid);

            // 캐릭터 정보 다시 쏴줌
            S_UserCharaDataRes res = new S_UserCharaDataRes();
            res.bitmask = cdata.bitmask;
            byte[] body = res.Serialize();
            Packet pkt = new Packet(PacketType.S_UserCharaDataRes, body, body.Length);
            packet.USER_TOKEN.Send(pkt);
        }
    }
}
