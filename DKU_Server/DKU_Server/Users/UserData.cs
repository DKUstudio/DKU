using DKU_Server.Connections.Tokens;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Users
{
    public class UserData
    {
        public UserToken UserToken { get; set; }

        public void Init(UserToken token)
        {

        }

        public void ProcessPacket(Packet packet)
        {

        }
    }
}
