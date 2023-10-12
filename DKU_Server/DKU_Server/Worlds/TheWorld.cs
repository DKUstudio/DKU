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

namespace DKU_Server.Worlds
{
    public class TheWorld
    {
        public Dictionary<long, LoginData> users;
        public int users_count => users.Count;
        public WorldBlock[] world_blocks = new WorldBlock[(int)WorldBlockType.Block_Count];

        public TheWorld()
        {
            users = new Dictionary<long, LoginData>();

            // 모든 방 초기화
            for (int i = 0; i < (int)WorldBlockType.Block_Count; i++)
            {
                world_blocks[i] = new WorldBlock(this);
            }
        }

        public UserToken FindUserToken(long uid)
        {
            if(users.ContainsKey(uid))
            {
                return users[uid].UserToken;
            }
            return null;
        }

        public void LoginUser(LoginData data)
        {
            lock (users)
            {
                Console.WriteLine("[Login] " + data.UserData.nickname);
                users.Add(data.UserData.uid, data);

                data.cur_world_block = (int)WorldBlockType.Dankook_University;   // 시작은 학교배경
                world_blocks[data.cur_world_block].EnterUser(data.UserData.uid);
            }
        }

        public void LogoutUser(long id)
        {
            lock (users)
            {
                if (users.ContainsKey(id))
                {
                    Console.WriteLine("[Logout] " + users[id].UserData.nickname);

                    short world_num = users[id].cur_world_block;
                    users.Remove(id);
                    world_blocks[world_num].ExitUser(id);
                }
            }
        }

        /// <summary>
        /// 모든 유저에게 채팅
        /// </summary>
        /// <param name="data"></param>
        public void ShootGlobalChat(ChatData data)
        {
            foreach (var user in users)
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
            bool user_find = users.TryGetValue(data.sender_uid, out var user);
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

            bool find_user = users.TryGetValue(data.recver_uid, out var user);
            if (find_user == false)
            {
                return;
            }
            user.UserToken.Send(packet);
        }

        public void ShootLocalUserPos(long uid, JVector3 pos)
        {
            bool user_find = users.TryGetValue(uid, out var user);
            if (user_find == false)
            {
                return;
            }

            short world_num = user.cur_world_block;
            world_blocks[world_num].ShootLocalUserPos(uid, pos);
        }
    }
}
