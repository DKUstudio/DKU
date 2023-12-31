﻿using System;
using System.Collections.Generic;
using System.Linq;
//using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore.Packets.var.qclient
{
    [Serializable]
    //[StructLayout(LayoutKind.Sequential, Pack = 1)] // pack=1: 1byte 단위로 데이터 크기를 맞춤
    public class QC_LoginReq : Data<QC_LoginReq>
    {
        public long wid;

        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = CommonDefine.MAX_PACKET_STRING_LENGTH)]
        public string id;
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = CommonDefine.MAX_PACKET_STRING_LENGTH)]
        public string pw;
    }
}
