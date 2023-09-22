using DKU_ServerCore;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DKU_DummyClient
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("============================");
            Console.WriteLine("           Client           ");
            Console.WriteLine("============================");

            Network network = new Network();
            network.Init();

            string host = Dns.GetHostName();
            IPHostEntry entry = Dns.GetHostEntry(host);
            IPAddress ipAddr = entry.AddressList[0];
            //Console.WriteLine(ipAddr);
            //network.Connect(ipAddr.ToString(), 7777);
            network.Connect(CommonDefine.IPv4_ADDRESS, CommonDefine.IP_PORT);




            while (true)
            {

            }
        }
    }
}
