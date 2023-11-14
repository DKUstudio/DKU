using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore.Packets.var.server
{
    [Serializable]
    public class S_WorldChangeAvailRes : Data<S_WorldChangeAvailRes>
    {
        // 0: success
        // 1: failed: full room
        // 2: failed: already started game
        public short success;
        public short room_number;
    }
}
