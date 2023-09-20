using System;
using System.Net;

namespace DKU_Server
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("============================");
            Console.WriteLine("           Server           ");
            Console.WriteLine("============================");

            ClientListener listener = new ClientListener();
            string host = Dns.GetHostName();
            IPHostEntry entry = Dns.GetHostEntry(host);
            IPAddress ipAddr = entry.AddressList[0];
            //Console.WriteLine(ipAddr);
            listener.Start(ipAddr.ToString(), 7777, 10);

            while (true) { }
        }
    }
}
