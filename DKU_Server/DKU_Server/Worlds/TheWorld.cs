using DKU_Server.Connections;
using DKU_Server.Connections.Tokens;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DKU_Server.Worlds
{
    public class TheWorld
    {
        public Dictionary<long, LoginData> uid_users;
        public int users_count => uid_users.Count;


        public WorldBlock[] world_blocks = new WorldBlock[(int)WorldBlockType.Block_Count];

        public TheWorld()
        {
            uid_users = new Dictionary<long, LoginData>();

            // 모든 방 초기화
            for (int i = 0; i < (int)WorldBlockType.Block_Count; i++)
            {
                world_blocks[i] = new WorldBlock(this);
            }
        }

        public UserToken FindUserToken(long uid)
        {
            if (uid_users.ContainsKey(uid))
            {
                return uid_users[uid].UserToken;
            }
            return null;
        }
        public void AddUidUser(long v_uid, LoginData v_data)
        {
            try
            {
                uid_users.Add(v_uid, v_data);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void RemoveUidUser(long v_uid)
        {
            if (uid_users.ContainsKey(v_uid))
            {
                Console.WriteLine($"[Logout] {uid_users[v_uid].UserData.nickname}");
                uid_users.Remove(v_uid);
            }
        }


        /// <summary>
        /// 모든 유저에게 채팅
        /// </summary>
        /// <param name="data"></param>
        public void ShootGlobalChat(ChatData data)
        {
            foreach (var user in uid_users)
            {
                S_ChatRes res = new S_ChatRes();
                res.chatData = data;
                byte[] body = res.Serialize();

                Packet packet = new Packet(PacketType.S_ChatRes, body, body.Length);
                user.Value.UserToken.Send(packet);
            }
        }

        /// <summary>
        /// 소속 월드 유저에게 채팅
        /// </summary>
        /// <param name="data"></param>
        public void ShootLocalChat(ChatData data)
        {
            bool user_find = uid_users.TryGetValue(data.sender_uid, out var user);
            if (user_find == false)
            {
                return;
            }

            short world_num = user.cur_world_block;
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
            user.UserToken.Send(packet);
        }

        public void ShootLocalUserPos(long uid, JVector3 pos)
        {
            bool user_find = uid_users.TryGetValue(uid, out var user);
            if (user_find == false)
            {
                return;
            }

            short world_num = user.cur_world_block;
            world_blocks[world_num].ShootLocalUserPos(uid, pos);
        }
    }
}
