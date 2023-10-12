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
        int sid;
        UserToken token;
        UserData data;

        public int SID => sid;
        public UserToken UserToken => token;
        public UserData UserData => data;

        /// <summary>
        /// 현재 위치한 월드의 인덱스
        /// </summary>
        public short cur_world_block;

        public LoginData() { }
        public LoginData(UserToken token, UserData data)
        {
            this.token = token;
            this.data = data;
        }
        public void SetSid(int v_sid)
        {
            sid = v_sid;
        }
        public void SetUserToken(UserToken v_token)
        {
            token = v_token;
        }
        public void SetUserData(UserData v_data)
        {
            data = v_data;
        }
    }
}
