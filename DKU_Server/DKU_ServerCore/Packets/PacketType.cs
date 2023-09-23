using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore.Packets
{
    public enum PacketType
    {
        TYPE_NONE = -1,

        GlobalChatReq,
        GlobalChatRes,

        PACKET_COUNT
    }
}
