using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore.Packets.var.client
{
    [Serializable]
    public class C_UserCharaDataChangeReq : Data<C_UserCharaDataChangeReq>
    {
        public long uid;
        public int changed_bitmask;
        public short changed_lastloginshift;
    }
}
