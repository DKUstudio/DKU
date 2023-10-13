  
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
		C_MyUserDataReq,
		C_PingReq,
		C_RegisterReq,
		C_StartAuthReq,
		C_StopWaitingReq,
		C_TryAuthReq,
		C_UserPosReq,


		S_ChatRes,
		S_FinallyLoggedInReq,
		S_LogoutRes,
		S_PingRes,
		S_StartAuthRes,
		S_TryAuthRes,
		S_UserPosRes,
		S_YourSidRes,


		Q_CurUsersCountReq,
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
