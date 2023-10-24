using DKU_Server.Connections;
using DKU_Server.Connections.Tokens;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Worlds
{
    public class WorldBlock
    {
        TheWorld the_world;
        public HashSet<long> cur_block_users_uid;
        public CommonDefine.WorldBlockType w_type;

        public WorldBlock(TheWorld world)
        {
            the_world = world;
            cur_block_users_uid = new HashSet<long>();
        }

        public void AddUid(long v_uid)
        {
            LogManager.Log($"[WorldBlock] {v_uid} entered {w_type}... {cur_block_users_uid.Count + 1}");
            cur_block_users_uid.Add(v_uid);
        }

        public void RemoveUid(long v_uid)
        {
            if (cur_block_users_uid.Contains(v_uid))
            {
                LogManager.Log($"[WorldBlock] {v_uid} exited {w_type}... {cur_block_users_uid.Count - 1}");

                cur_block_users_uid.Remove(v_uid);
            }
        }

        public void ShootLocalChat(ChatData data)
        {
            S_ChatRes res = new S_ChatRes();
            res.chatData = data;
            byte[] body = res.Serialize();

            Packet packet = new Packet(PacketType.S_ChatRes, body, body.Length);
            foreach (var user in cur_block_users_uid)
            {
                bool find_user = the_world.uid_users.TryGetValue(user, out UserToken token);
                if (find_user == false)
                {
                    // TODO 비유효 유저 검사
                    continue;
                }
                token.Send(packet);
            }
        }

        public void ShootLocalPlayerPos(long uid, JVector3 pos, JVector3 rot)
        {
            //LogManager.Log($"[Move] {uid} moved");
            try
            {
                S_PlayerPosRes res = new S_PlayerPosRes();
                res.uid = uid;
                res.pos = pos;
                res.rot = rot;
                byte[] body = res.Serialize();

                Packet packet = new Packet(PacketType.S_PlayerPosRes, body, body.Length);
                lock (cur_block_users_uid)
                {
                    foreach (long item in cur_block_users_uid)
                    {
                        //LogManager.Log($"[LocalPos] check {item}, uid {uid}");
                        if (uid == item)
                        {
                            //LogManager.Log($"[LocalPos] {uid} == {item}");
                            continue;
                        }

                        bool find_user = the_world.uid_users.TryGetValue(item, out UserToken token);
                        if (find_user == false)
                        {
                            // TODO 비유효 유저 검사
                            LogManager.Log($"[LocalPos] no target found {item}");
                            continue;
                        }
                        token.Send(packet);
                    }
                }
            }
            catch (Exception e)
            {
                LogManager.Log(e.ToString());
            }
        }
    }
}
