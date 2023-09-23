using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore.Packets
{
    [Serializable]
    public enum PacketType
    {
        TYPE_NONE = -1,

        UserDataRes,
        LogoutReq,
        GlobalChatReq,
        GlobalChatRes,

        PACKET_COUNT
    }
}
