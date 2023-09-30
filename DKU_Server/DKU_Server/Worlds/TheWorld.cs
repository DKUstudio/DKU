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

        public TheWorld()
        {
            users = new Dictionary<long, LoginData>();
        }

        public void LoginUser(LoginData data)
        {
            lock (users)
            {
                Console.WriteLine("[Login] " + data.UserData.nickname);
                data.world_block_number = (int)WorldBlockType.Dankook_University;   // 시작은 학교배경
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
                    // TODO 소속되어 있던 월드 블록에 등록된 uid 지우는 절차 필요
                    users.Remove(id);
                }
            }
        }

        /// <summary>
        /// 특정 유저에게 귓속말
        /// </summary>
        /// <param name="v_msg">메시지</param>
        /// <param name="v_udata">발신자</param>
        public void ShootWhisperChat(string v_msg, UserData v_udata)
        {

        }
    }
}
