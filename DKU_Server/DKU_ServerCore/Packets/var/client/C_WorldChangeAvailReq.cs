using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore.Packets.var.client
{
    [Serializable]
    public class C_WorldChangeAvailReq : Data<C_WorldChangeAvailReq>
    {
        public UserData udata;

        /// <summary>
        /// using DKU_ServerCore;
        /// CommonDefine의 WorldBlockType을 short형으로 캐스팅해서 넣어줄것
        /// </summary>
        public short room_number;
    }
}
