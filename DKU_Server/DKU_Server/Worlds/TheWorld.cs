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
        public Dictionary<int, LoginData> sid_users;
        public Dictionary<long, LoginData> uid_users;
        public int users_count => sid_users.Count + uid_users.Count;


        public WorldBlock[] world_blocks = new WorldBlock[(int)WorldBlockType.Block_Count];

        public TheWorld()
        {
            sid_users = new Dictionary<int, LoginData>();
            uid_users = new Dictionary<long, LoginData>();

            // 모든 방 초기화
            for (int i = 0; i < (int)WorldBlockType.Block_Count; i++)
            {
                world_blocks[i] = new WorldBlock(this);
            }
        }

        public UserToken FindUserToken(long uid)
        {
            if(uid_users.ContainsKey(uid))
            {
                return uid_users[uid].UserToken;
            }
            return null;
        }

        public void AddSidUser(UserToken token)
        {
            int sid = GenSid();
            LoginData ldata = new LoginData();
            ldata.SetSid(sid);
            ldata.SetUserToken(token);
            sid_users.Add(sid, ldata);

            S_YourSidRes res = new S_YourSidRes();
            res.sid = sid;
            byte[] body = res.Serialize();

            Console.WriteLine($"[GameServer] your sid: {sid}");
            if(token == null)
            {
                Console.WriteLine("[Connection] userToken is null");
            }
            Packet pkt = new Packet(PacketType.S_YourSidRes, body, body.Length);
            token.Send(pkt);
        }
        public void MoveSidToUidUsers(int sid, UserData v_udata)
        {
            sid_users[sid].SetUserData(v_udata);
            LoginData ldata = sid_users[sid];
            sid_users.Remove(sid);

            uid_users.Add(ldata.UserData.uid, ldata);
            Console.WriteLine("[Login] uid confirm success");
        }

        int sid_gen = 0;
        Stack<int> sid_pool = new Stack<int>();
        int GenSid()
        {
            if(sid_pool.Count > 0)
            {
                return sid_pool.Pop();
            }
            return sid_gen++;
        }
        public void ReturnSid(int v_sid)
        {
            sid_pool.Push(v_sid);
        }

        /// <summary>
        /// 모든 유저에게 채팅
        /// </summary>
        /// <param name="data"></param>
        public void ShootGlobalChat(ChatData data)
        {
            foreach (var user in sid_users)
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
