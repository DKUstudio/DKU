using DKU_Server.Connections.Tokens;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Connections
{
    public class SPacket : Packet
    {
        private UserToken userToken;

        public SPacket() { }
        public SPacket(PacketType type, byte[] data, int len) : base(type, data, len)
        {
            SetData(type, data, len);
        }

        public UserToken USER_TOKEN => userToken;

        public void SetUserToken(UserToken token)
        {
            userToken = token;
        }
    }
}
