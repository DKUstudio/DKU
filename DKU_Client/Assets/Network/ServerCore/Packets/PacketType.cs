  
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
        
		C_ChatReq,
		C_LoginReq,
		C_LogoutReq,
		C_PingReq,
		C_RegisterReq,
		C_StopWaitingReq,


		S_ChatRes,
		S_LoginRes,
		S_LogoutRes,
		S_PingRes,
		S_RegisterRes,
		S_WaitingIdRes,

        PACKET_COUNT
    }
}
