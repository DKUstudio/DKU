using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore
{
    public static class CommonDefine
    {
        //
#if !RELEASE
        public const string IPv4_ADDRESS = "172.30.1.47";
#else
        public const string IPv4_ADDRESS = "172.190.38.29";
#endif
        public const int IP_PORT = 53;

        // 패킷에 담는 문자열의 최대 길이
        public const int MAX_PACKET_STRING_LENGTH = 100;

        // 최대 동접자 수
        public const int MAX_CONNECTION = 50;

        // 소켓에서 한번에 보낼 수 있는 버퍼 크기 : 4K
        public const int SOCKET_BUFFER_SIZE = 4096;

        // 
        public const int COMPLETE_MESSAGE_SIZE_CLIENT = 1024 * 20;
        
        // 패킷의 헤더 크기 : 4 byte
        public const int HEADER_SIZE = 4;

        static public string ToReadableByteArray(byte[] bytes)
        {
            return string.Join(",", bytes);
        }
    }
}
