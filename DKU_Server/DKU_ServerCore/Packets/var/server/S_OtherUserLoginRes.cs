using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore.Packets.var.server
{
    [Serializable]
    public class S_OtherUserLoginRes : Data<S_OtherUserLoginRes>
    {
        public long login_uid;
        public UserData udata;
    }
}
