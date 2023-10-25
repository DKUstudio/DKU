using System;
using System.Collections.Generic;
using System.Linq;
//using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore.Packets.var.client
{
    [Serializable]
    //[StructLayout(LayoutKind.Sequential, Pack = 1)] // pack=1: 1byte 단위로 데이터 크기를 맞춤
    public class C_ChatReq : Data<C_ChatReq>
    {
        public ChatData chatData;

        public C_ChatReq()
        {
            chatData = new ChatData();
        }
    }
}
