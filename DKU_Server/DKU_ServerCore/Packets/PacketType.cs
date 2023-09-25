  
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
        
		C_GlobalChatReq,
		C_LoginReq,
		C_LogoutReq,
		C_PingReq,
		C_RegisterReq,


		S_AcceptIdRes,
		S_GlobalChatRes,
		S_LoginRes,
		S_LogoutRes,
		S_PingRes,
		S_RegisterRes,

        PACKET_COUNT
    }
}
