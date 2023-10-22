using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DKU_Server.Connections;
using DKU_Server.DBs;
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
                    LogManager.Log(val.ToString());
                    listener.Start(val.ToString(), CommonDefine.IP_PORT, CommonDefine.MAX_CONNECTION);
                    break;
                }
            }

            while (true) 
            { 
                Thread.Sleep(100);
            }
        }
    }
}
