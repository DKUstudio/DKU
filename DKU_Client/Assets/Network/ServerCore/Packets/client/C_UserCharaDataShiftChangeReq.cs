using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore.Packets.var.client
{
    [Serializable]
    public class C_UserCharaDataShiftChangeReq : Data<C_UserCharaDataShiftChangeReq>
    {
        public long uid;
        public short lastloginshift;
    }
}
