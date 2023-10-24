  
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
		C_GetWorldUsersDataReq,
		C_LoginReq,
		C_LogoutReq,
		C_MyUserDataReq,
		C_PingReq,
		C_PlayerPosReq,
		C_RegisterReq,
		C_StartAuthReq,
		C_TryAuthReq,


		S_ChatRes,
		S_ConnectionTimeoutRes,
		S_GetWorldUsersDataRes,
		S_PingRes,
		S_PlayerPosRes,
		S_StartAuthRes,
		S_TryAuthRes,


		Q_GoToGameServerRes,
		Q_LoginRes,
		Q_QueueStartRes,
		Q_RegisterRes,
		Q_WaitForLoginRes,
		Q_YourWidRes,
		QC_LoginReq,
		QC_LogoutReq,
		QC_RegisterReq,


		GS_CurUsersCountRes,
		GS_QueueStartReq,

        PACKET_COUNT
    }
}
