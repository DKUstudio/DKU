﻿using System;
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

        C_RegisterReq,
        C_LoginReq,
        C_LogoutReq,
        C_GlobalChatReq,


        S_AcceptIdRes,
        S_RegisterRes,
        S_LoginRes,
        S_LogoutRes,
        S_GlobalChatRes,

        PACKET_COUNT
    }
}