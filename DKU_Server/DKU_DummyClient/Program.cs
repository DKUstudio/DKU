using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var;
using DKU_ServerCore.Packets.var.client;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DKU_DummyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("============================");
            Console.WriteLine("           Client           ");
            Console.WriteLine("   type \"exit\" to EXIT    ");
            Console.WriteLine("============================");

            Network.Instance.Connect(CommonDefine.IPv4_ADDRESS, CommonDefine.IP_PORT);

            string id, pw;

            if (args.Length > 0)
            {
                id = args[0];
                pw = args[1];
            }
            else
            {
                id = method2(8);
                pw = "1111";
            }
            Thread.Sleep(1000);
            Register(id, pw);
            Thread.Sleep(1000);
            Login(id, pw);

            //Task task = new Task(Ping);
            //task.Start();

            while (true)
            {
                ConsoleJob();

            }
        }

        #region console
        private static Random random = new Random();
        public static string method2(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(characters, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        static void ConsoleJob()
        {
            string txt = Console.ReadLine();
            if (txt == "exit")
            {
                C_LogoutReq outreq = new C_LogoutReq();
                outreq.uid = Network.Instance.m_user_data.uid;
                byte[] oserial = outreq.Serialize();

                Packet outpkt = new Packet(PacketType.C_LogoutReq, oserial, oserial.Length);
                Network.Instance.Send(outpkt);
                return;
            }

            C_GlobalChatReq req = new C_GlobalChatReq();
            req.udata = Network.Instance.m_user_data;
            req.chat_message = txt;
            byte[] serial = req.Serialize();

            Packet pkt = new Packet(PacketType.C_GlobalChatReq, serial, serial.Length);
            Network.Instance.Send(pkt);
        }
        static void Ping()
        {
            while (true)
            {
                C_PingReq req = new C_PingReq();
                req.send_ms = DateTime.Now.Millisecond;
                byte[] serial = req.Serialize();

                Packet pkt = new Packet(PacketType.C_PingReq, serial, serial.Length);
                Network.Instance.Send(pkt);
                Thread.Sleep(1000);
            }
        }
        static void Register(string id, string pw)
        {
            C_RegisterReq req = new C_RegisterReq();
            req.accept_id = Network.Instance.m_accept_id;
            req.id = id;
            req.pw = pw;
            req.nickname = id;
            byte[] serial = req.Serialize();

            Packet pkt = new Packet();
            pkt.SetData(PacketType.C_RegisterReq, serial, serial.Length);

            Network.Instance.Send(pkt);
        }

        static void Login(string id, string pw)
        {
            C_LoginReq req = new C_LoginReq();
            req.accept_id = Network.Instance.m_accept_id;
            req.id = id;
            req.pw = pw;
            byte[] serial = req.Serialize();

            Packet pkt = new Packet();
            pkt.SetData(PacketType.C_LoginReq, serial, serial.Length);

            Network.Instance.Send(pkt);
        }
        #endregion
    }
}
