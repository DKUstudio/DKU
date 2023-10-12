using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_LoginQueue
{
    public class LoginData
    {
        UserToken? token;
        UserData? data;

        public UserToken UserToken => token;
        public UserData UserData => data;

        public LoginData() { }
        public LoginData(UserToken token, UserData data)
        {
            this.token = token;
            this.data = data;
        }
    }
}
