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
    }
}
