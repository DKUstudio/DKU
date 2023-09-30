using DKU_ServerCore.Packets;
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

        public virtual void EnterUser(long v_uid)
        {
            users_uid.Add(v_uid);
        }

        /// <summary>
        /// 현재 월드에 전체채팅 뿌림
        /// </summary>
        /// <param name="v_msg">메시지</param>
        /// <param name="v_udata">발신자</param>
        public void ShootGlobalChatting(string v_msg, UserData v_udata)
        {
            string msg = v_msg;
            UserData udata = v_udata;
        }

        /// <summary>
        /// 해당 월드에 지역채팅 뿌림
        /// </summary>
        /// <param name="v_msg">메시지</param>
        /// <param name="v_udata">발신자</param>
        public virtual void ShootLocalChatting(string v_msg, UserData v_udata)
        {

        }
    }
}
