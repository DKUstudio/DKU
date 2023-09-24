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
        public void AddUser(LoginData data)
        {
            lock (users)
            {
                Console.WriteLine("[Login] " + data.UserData.nickname);
                users.Add(data.UserData.uid, data);
            }
        }

        public void RemoveUser(long id)
        {
            lock (users)
            {
                Console.WriteLine("[Logout] " + users[id].UserData.nickname);
                Packet pkt = new Packet();
                pkt.m_type = (short)PacketType.S_LogoutRes;
                pkt.SetData(new byte[] { }, 0);
                users[id].UserToken.Send(pkt);

                users.Remove(id);
            }
        }

        public void GlobalChat(C_GlobalChatReq req)
        {
            // 현재 접속중인 아이디인지 확인하는 절차?
            if (users.ContainsKey(req.udata.uid) == false)
                return;

            S_GlobalChatRes res = new S_GlobalChatRes();
            res.udata = req.udata;
            res.chat_message = req.chat_message;
            byte[] serial = res.Serialize();

            Packet pkt = new Packet(PacketType.S_GlobalChatRes, serial, serial.Length);

            foreach (var user in users)
            {
                user.Value.UserToken.Send(pkt);
            }
        }
    }
}
