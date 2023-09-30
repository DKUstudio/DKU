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
        UserToken token;
        UserData data;

        public UserToken UserToken => token;
        public UserData UserData => data;

        /// <summary>
        /// 현재 위치한 월드의 인덱스
        /// </summary>
        public int cur_world_block;

        public LoginData() { }
        public LoginData(UserToken token, UserData data)
        {
            this.token = token;
            this.data = data;
        }
    }
}
