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

        public UserToken Token => token;
        public UserData Data => data;

        public LoginData(UserToken token, UserData data)
        {
            this.token = token;
            this.data = data;
        }
    }
}
