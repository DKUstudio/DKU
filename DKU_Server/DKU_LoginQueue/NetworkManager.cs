using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.gserver;
using DKU_ServerCore.Packets.var.queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DKU_LoginQueue
{
    public class NetworkManager
    {
        private static NetworkManager instance;
        public static NetworkManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NetworkManager();
                }
                return instance;
            }
        }

        public UserToken? m_game_server;

        public GamePacketHandler m_game_packet_handler = new GamePacketHandler();

        public Dictionary<long, UserToken> m_wid_list = new Dictionary<long, UserToken>();

        public void onNewClient(Socket client_socket, SocketAsyncEventArgs args)
        {
            UserToken token = new UserToken();
            token.Init();

            client_socket.NoDelay = true;
            client_socket.ReceiveTimeout = 60 * 1000;
            client_socket.SendTimeout = 60 * 1000;
            token.m_socket = client_socket;
            token.StartRecv();

            long wid = Getwid();
            m_wid_list.Add(wid, token);

            Q_YourWidRes res = new Q_YourWidRes();
            res.wid = wid;
            byte[] body = res.Serialize();

            Packet packet = new Packet(PacketType.Q_YourWidRes, body, body.Length);
            token.Send(packet);

            Console.WriteLine("[onNewClient] new client came");
        }

        Stack<long> m_wid_pool = new Stack<long>();
        long wid_gen = 0;
        long Getwid()
        {
            if (m_wid_pool.Count > 0)
                return m_wid_pool.Pop();
            return wid_gen++;
        }
        public void Returnwid(long wid)
        {
            m_wid_pool.Push(wid);
        }

        public void NewGameServer(long wid)
        {
            if (m_wid_list.ContainsKey(wid))
            {
                m_game_server = m_wid_list[wid];
                m_wid_list.Remove(wid);
            }

        }
    }
}
