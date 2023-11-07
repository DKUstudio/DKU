using DKU_Server.Connections;
using DKU_Server.Connections.Tokens;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.server;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static DKU_ServerCore.CommonDefine;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DKU_Server.Worlds
{
    public class TheWorld
    {
        public Dictionary<long, UserToken> uid_users;
        public int users_count => uid_users.Count;

        public WorldBlock[] world_blocks = new WorldBlock[(int)WorldBlockType.Block_Count];

        public TheWorld()
        {
            uid_users = new Dictionary<long, UserToken>();

            // 모든 방 초기화
            for (int i = 0; i < (int)WorldBlockType.Block_Count; i++)
            {
                world_blocks[i] = new WorldBlock(this, i);
                world_blocks[i].w_type = i;
            }
        }

        public UserToken FindUserToken(long uid)
        {
            if (uid_users.ContainsKey(uid))
            {
                return uid_users[uid];
            }
            return null;
        }
        public void AddNewUidUser(long v_uid, UserToken v_data)
        {
            try
            {
                if(uid_users.ContainsKey(v_uid))
                {
                    // TODO 중복로그인 예외처리
                }
                uid_users.Add(v_uid, v_data);
                v_data.ldata = new LoginData();
                v_data.ldata.cur_world_block = 0;
                world_blocks[0].AddUid(v_uid, v_data.udata);
                LogManager.Log($"[Login] Hello, {v_data.udata.nickname}");
            }
            catch (Exception e)
            {
                LogManager.Log(e.ToString());
            }
        }
        public void RemoveUidUser(long v_uid)
        {
            if (uid_users.ContainsKey(v_uid))
            {
                LogManager.Log($"[Logout] {uid_users[v_uid].udata.nickname}");
                short world_num = uid_users[v_uid].ldata.cur_world_block;
                world_blocks[world_num].RemoveUid(v_uid);
                uid_users.Remove(v_uid);
            }
        }


        /// <summary>
        /// 모든 유저에게 채팅
        /// </summary>
        /// <param name="data"></param>
        public void ShootGlobalChat(ChatData data)
        {
            S_ChatRes res = new S_ChatRes();
            res.chatData = data;
            byte[] body = res.Serialize();

            Packet packet = new Packet(PacketType.S_ChatRes, body, body.Length);
            foreach (var user in uid_users)
            {
                user.Value.Send(packet);
            }
        }

        /// <summary>
        /// 소속 월드 유저에게 채팅
        /// </summary>
        /// <param name="data"></param>
        public void ShootLocalChat(ChatData data)
        {
            bool user_find = uid_users.TryGetValue(data.sender_data.uid, out var user);
            if (user_find == false)
            {
                LogManager.Log($"[LocalChat] found no sender user {data.sender_data.uid}");
                return;
            }

            short world_num = user.ldata.cur_world_block;
            //LogManager.Log($"[LocalChat] shoot local chat at {world_num}");
            world_blocks[world_num].ShootLocalChat(data);
        }

        /// <summary>
        /// 특정 유저에게 귓속말
        /// </summary>
        public void ShootWhisperChat(ChatData data)
        {
            S_ChatRes res = new S_ChatRes();
            res.chatData = data;
            byte[] body = res.Serialize();

            Packet packet = new Packet(PacketType.S_ChatRes, body, body.Length);

            bool find_user = uid_users.TryGetValue(data.recver_uid, out var user);
            if (find_user == false)
            {
                return;
            }
            user.Send(packet);
        }

        public void ShootPlayerPos(long uid, JVector3 pos, JVector3 rot)
        {
            try
            {
                bool user_find = uid_users.TryGetValue(uid, out var user);
                if (user_find == false)
                {
                    LogManager.Log($"[ShootPlayerPos] no such user {uid}");
                    return;
                }
                user.ldata.cur_pos = pos;
                user.ldata.cur_rot = rot;

                short world_num = user.ldata.cur_world_block;
                world_blocks[world_num].ShootLocalPlayerPos(uid, pos, rot);
            }
            catch (Exception e)
            {
                LogManager.Log(e.ToString());
            }
        }

        public List<UserData> GetCurWorldUserDatas(long v_uid)
        {
            List<UserData> ret = new List<UserData>();

            bool find_user = uid_users.TryGetValue(v_uid, out var user);
            if (find_user == false)
            {
                LogManager.Log($"[GetCurWorldUserDatas] no such user {v_uid}");
                return ret;
            }

            short world_num = user.ldata.cur_world_block;
            foreach (long item in world_blocks[world_num].cur_block_users_uid)
            {
                if (item == v_uid)
                    continue;
                bool find_user2 = uid_users.TryGetValue(item, out var user2);
                if (find_user2 == false)
                {
                    LogManager.Log($"[GetCurWorldUserDatas] no such user {v_uid}");
                    continue;
                }
                ret.Add(user2.udata);
            }

            return ret;
        }
    }
}
