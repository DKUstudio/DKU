using DKU_Server.Connections;
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
        private static TheWorld instance;
        public static TheWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TheWorld();
                }
                return instance;
            }
        }

        public Dictionary<long, LoginData> users;
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

        public void LoginUser(LoginData data)
        {
            lock (users)
            {
                Console.WriteLine("[Login] " + data.UserData.nickname);
                data.cur_world_block = (int)WorldBlockType.Dankook_University;   // 시작은 학교배경
                users.Add(data.UserData.uid, data);
            }
        }

        public void RemoveUser(long id)
        {
            lock (users)
            {
                if (users.ContainsKey(id))
                {
                    Console.WriteLine("[Logout] " + users[id].UserData.nickname);
                    // TODO 소속되어 있던 월드 블록에서 등록된 uid 지우는 절차 필요
                    users.Remove(id);
                }
            }
        }

        /// <summary>
        /// 모든 유저에게 채팅
        /// </summary>
        /// <param name="data"></param>
        public void ShootGlobalChat(ChatData data)
        {
            foreach(var user in users)
            {
                // TODO 모든 유저에게 채팅 패킷 보내는 작업 필요
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
            // TODO 해당 월드 유저들에게 채팅 패킷 보내는 작업 필요
        }

        /// <summary>
        /// 특정 유저에게 귓속말
        /// </summary>
        public void ShootWhisperChat(ChatData data)
        {
            // TODO 특정 유저한테 귓속말 보내는 작업 필요
        }
    }
}
