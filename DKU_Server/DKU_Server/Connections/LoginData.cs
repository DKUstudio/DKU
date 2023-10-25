using DKU_Server.Connections.Tokens;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Connections
{
    public class LoginData
    {
        /// <summary>
        /// 현재 위치한 월드의 인덱스
        /// </summary>
        public short cur_world_block;
        public JVector3 cur_pos;
        public JVector3 cur_rot;

        public LoginData()
        {
            cur_world_block = 0;
        }
    }
}
