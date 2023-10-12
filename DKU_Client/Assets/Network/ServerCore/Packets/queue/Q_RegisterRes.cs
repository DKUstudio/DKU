using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore.Packets.var.queue
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)] // pack=1: 1byte 단위로 데이터 크기를 맞춤
    public class Q_RegisterRes : Data<Q_RegisterRes>
    {
        // 0: 성공
        // 1: id 중복
        // 2: nick 중복
        // 3: sql error
        public short success;
    }
}
