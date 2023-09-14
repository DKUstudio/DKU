using System;
using System.Net;
using System.Net.Sockets;
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

            Console.WriteLine("Client: " + ipAddr);

            Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            SocketAsyncEventArgs _args = new SocketAsyncEventArgs();
            _args.RemoteEndPoint = endPoint;
            _args.UserToken = socket;

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Hello world");
            _args.SetBuffer(buffer, 0, buffer.Length);
            socket.SendAsync(_args);

            while (true)
            {
                // 연결 유지
            }
        }
    }
}
