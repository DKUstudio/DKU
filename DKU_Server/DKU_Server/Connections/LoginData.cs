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

        public int world_block_number;  // 현재 위치한 월드타입 인덱스

        public LoginData() { }
        public LoginData(UserToken token, UserData data)
        {
            this.token = token;
            this.data = data;
        }
    }
}
