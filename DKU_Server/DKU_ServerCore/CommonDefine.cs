using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore
{
    public static class CommonDefine
    {
        public const int MAX_PACKET_STRING_LENGTH = 100;
        public const int MAX_CONNECTION = 50;
        public const int SOCKET_BUFFER_SIZE = 4096;
        public const int COMPLETE_MESSAGE_SIZE_CLIENT = 1024 * 20;
        public const Int16 HEADER_SIZE = 4;
    }
}
