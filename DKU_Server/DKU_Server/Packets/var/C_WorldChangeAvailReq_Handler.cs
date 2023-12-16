
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKU_Server.Connections;
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore;

namespace DKU_Server.Packets.var
{
    public class C_WorldChangeAvailReq_Handler
    {
        public static void Method(SPacket packet)
        {
            C_WorldChangeAvailReq req = Data<C_WorldChangeAvailReq>.Deserialize(packet.m_data);

            S_WorldChangeAvailRes res = new S_WorldChangeAvailRes();
            // 이미 플레이중인 방...실패
            if (NetworkManager.Instance.world.world_blocks[req.room_number].mini_game.isPlaying == true)
            {
                // 오류코드 1
                res.success = 1;
                res.room_number = req.room_number;
                byte[] body = res.Serialize();
                Packet pkt = new Packet(PacketType.S_WorldChangeAvailRes, body, body.Length);
                // 패킷 반사
                packet.USER_TOKEN.Send(pkt);

                return;
            }

            // 성공
            res.success = 0;
            res.room_number = req.room_number;
            byte[] body1 = res.Serialize();
            Packet pkt1 = new Packet(PacketType.S_WorldChangeAvailRes, body1, body1.Length);
            // 패킷 반사
            packet.USER_TOKEN.Send(pkt1);

            try
            {
                NetworkManager.Instance.world.ChangeUserWorldBlock(req.udata.uid, req.udata, req.from_room_number, req.room_number);
                NetworkManager.Instance.world.uid_users[req.udata.uid].ldata.cur_world_block = req.room_number;
            }
            catch (Exception e)
            {
                LogManager.Log($"[WorldChange] user {req.udata.uid} data match failed");
            }
        }
    }
}
