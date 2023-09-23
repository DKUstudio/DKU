using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DKU_DummyClient
{
    class Program
    {
        static Network network;
        static void Main(string[] args)
        {
            Console.WriteLine("============================");
            Console.WriteLine("           Client           ");
            Console.WriteLine("============================");

            network = new Network();
            network.Init();

            //string host = Dns.GetHostName();
            //IPHostEntry entry = Dns.GetHostEntry(host);
            //IPAddress ipAddr = entry.AddressList[0];
            //Console.WriteLine(ipAddr);
            //network.Connect(ipAddr.ToString(), 7777);
            network.Connect(CommonDefine.IPv4_ADDRESS, CommonDefine.IP_PORT);




            while (true)
            {
                string str = Console.ReadLine();
                //Console.WriteLine(str);
                if(str == "exit")
                {
                    LogoutReq logoutReq = new LogoutReq();
                    logoutReq.uid = network.m_user_data.uid;
                    byte[] bytes = logoutReq.Serialize();

                    Packet packet = new Packet();
                    packet.SetData(PacketType.LogoutReq, bytes, bytes.Length);
                    network.Send(packet);
                    continue;
                }

                GlobalChatRes chat = new GlobalChatRes();
                chat.chat_message = str;
                byte[] serial = chat.Serialize();
                //Console.WriteLine(CommonDefine.ToReadableByteArray(serial));

                Packet pkt = new Packet();
                pkt.SetData(serial, serial.Length);
                pkt.m_type = (int)PacketType.GlobalChatReq;
                network.Send(pkt);
            }
        }
    }
}
