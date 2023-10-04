using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DKU_Server.Connections;
using DKU_ServerCore;

namespace DKU_Server
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("============================");
            Console.WriteLine("         DKU Server         ");
            Console.WriteLine("============================");

            NetworkManager.Instance.Init();

            ClientListener listener = new ClientListener();

            //string host = Dns.GetHostName();
            IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());
            foreach(var val in entry.AddressList)
            {
                if(val.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine(val);
                    listener.Start(val.ToString(), CommonDefine.IP_PORT, CommonDefine.MAX_CONNECTION);
                    break;
                }
            }
            //IPAddress ipAddr = entry.AddressList[0];
            //Console.WriteLine(ipAddr);
            //Console.WriteLine(ipAddr);
            //listener.Start(ipAddr.ToString(), CommonDefine.IP_PORT, 10);
            //listener.Start("192.168.0.4", CommonDefine.IP_PORT, 10);

            while (true) 
            { 
                Thread.Sleep(1000);
                //NetworkManager.Instance.TestPing();
                //NetworkManager.Instance.TokensCount();
            }
        }
    }
}
