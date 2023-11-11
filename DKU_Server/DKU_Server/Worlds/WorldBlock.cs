using DKU_Server.Connections;
using DKU_Server.Connections.Tokens;
using DKU_Server.Worlds.MiniGames;
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
        public TheWorld THE_WORLD => the_world;
        public HashSet<long> cur_block_users_uid;
        public int w_type;

        public MiniGame mini_game;
        public Action user_entered_event;

        public WorldBlock(TheWorld world, int world_type)
        {
            the_world = world;
            cur_block_users_uid = new HashSet<long>();

            w_type = world_type;
            mini_game = MiniGame.Gen_MiniGame(this, (short)world_type);
        }

        public void AddUid(long v_uid, UserData v_udata)
        {
            LogManager.Log($"[WorldBlock] {v_uid} entered {w_type}... {cur_block_users_uid.Count + 1}");

            // 미니게임에 유저 입장을 알림
            user_entered_event.Invoke();

            try
            {
                // 해당 월드에 유저가 들어왔다는 사실을 다른 유저들에게 알림
                S_OtherUserLoginRes res = new S_OtherUserLoginRes();
                res.login_uid = v_uid;
                res.udata = v_udata;
                byte[] body = res.Serialize();
                Packet packet = new Packet(PacketType.S_OtherUserLoginRes, body, body.Length);
                foreach (var item in cur_block_users_uid)
                {
                    var usr = the_world.FindUserToken(item);
                    if (usr != null)
                    {
                        usr.Send(packet);
                    }
                }

            }
            catch (Exception e)
            {

            }
            cur_block_users_uid.Add(v_uid);
        }

        public void RemoveUid(long v_uid)
        {
            if (cur_block_users_uid.Contains(v_uid))
            {
                LogManager.Log($"[WorldBlock] {v_uid} exited {w_type}... {cur_block_users_uid.Count - 1}");

                try
                {
                    // 해당 월드의 유저 제거
                    S_OtherUserLogoutRes res = new S_OtherUserLogoutRes();
                    res.logout_uid = v_uid;
                    byte[] body = res.Serialize();
                    Packet packet = new Packet(PacketType.S_OtherUserLogoutRes, body, body.Length);

                    foreach (var usr in cur_block_users_uid)
                    {
                        var found_usr = the_world.FindUserToken(usr);
                        if (found_usr != null)
                        {
                            found_usr.Send(packet);
                        }
                    }


                }
                catch (Exception e)
                {
                    LogManager.Log($"[OtherLogout] {e.ToString()}");
                }
                cur_block_users_uid.Remove(v_uid);

            }
        }

        public void ShootLocalChat(ChatData data)
        {
            S_ChatRes res = new S_ChatRes();
            res.chatData = data;
            byte[] body = res.Serialize();

            Packet packet = new Packet(PacketType.S_ChatRes, body, body.Length);

            List<long> error_user = new List<long>();
            foreach (var user in cur_block_users_uid)
            {
                bool find_user = the_world.uid_users.TryGetValue(user, out UserToken token);
                if (find_user == false)
                {
                    error_user.Add(user);
                    continue;
                }
                token.Send(packet);
            }
            foreach (long val in error_user)
            {
                try
                {
                    cur_block_users_uid.Remove(val);
                }
                catch (Exception e)
                {

                }
            }
        }

        public void ShootLocalPlayerPos(long uid, JVector3 pos, JVector3 rot)
        {
            //LogManager.Log($"[Move] {uid} moved");
            try
            {
                S_UserPosRes res = new S_UserPosRes();
                res.uid = uid;
                res.pos = pos;
                res.rot = rot;
                byte[] body = res.Serialize();

                Packet packet = new Packet(PacketType.S_UserPosRes, body, body.Length);
                List<long> error_user = new List<long>();
                foreach (long item in cur_block_users_uid)
                {
                    if (uid == item)
                    {
                        continue;
                    }

                    bool find_user = the_world.uid_users.TryGetValue(item, out UserToken token);
                    if (find_user == false)
                    {
                        error_user.Add(item);
                        continue;
                    }
                    token.Send(packet);
                }
                foreach (long val in error_user)
                {
                    try
                    {
                        cur_block_users_uid.Remove(val);
                    }
                    catch (Exception e)
                    {

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
