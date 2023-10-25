using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore.Packets.var.server
{
    [Serializable]
    public class S_OtherUserLogoutRes : Data<S_OtherUserLogoutRes>
    {
        public long logout_uid;
    }
}
