using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore.Packets.var.client
{
    [Serializable]
    public class C_UserAnimChangeReq : Data<C_UserAnimChangeReq>
    {
        public long uid;
        public string animName;
    }
}
