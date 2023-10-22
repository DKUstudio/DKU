using DKU_Server.Connections;
using DKU_Server.Connections.Tokens;
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
        public List<long> users_uid;

        public WorldBlock(TheWorld world)
        {
            the_world = world;
            users_uid = new List<long>();
        }

        public void EnterUser(long v_uid)
        {
            users_uid.Add(v_uid);
        }

        public void ExitUser(long v_uid)
        {
            users_uid.Remove(v_uid);
        }

        public void ShootLocalChat(ChatData data)
        {
            S_ChatRes res = new S_ChatRes();
            res.chatData = data;
            byte[] body = res.Serialize();

            Packet packet = new Packet(PacketType.S_ChatRes, body, body.Length);
            foreach (var user in users_uid)
            {
                bool find_user = the_world.uid_users.TryGetValue(user, out UserToken token);
                if(find_user == false)
                {
                    // TODO 비유효 유저 검사
                    continue;
                }
                token.Send(packet);
            }
        }

        public void ShootLocalUserPos(long uid, JVector3 pos)
        {
            S_UserPosRes res = new S_UserPosRes();
            res.uid = uid;
            res.v3 = pos;
            byte[] body = res.Serialize();

            Packet packet = new Packet(PacketType.S_UserPosRes, body, body.Length);
            foreach (var user in users_uid)
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
    }
}
