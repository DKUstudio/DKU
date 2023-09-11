using System;
using System.Net;
using DKU_ServerCore;

namespace DKU_DummyClient
{
    class Program
    {

        static void Main(string[] args)
        {
            // 주소 설정
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);


            while(true)
            {
                // 연결 유지
            }
        }
    }
}
