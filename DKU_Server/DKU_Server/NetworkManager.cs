using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DKU_Server.Connections;
using DKU_Server.Connections.Tokens;
using DKU_Server.DBs;
using DKU_Server.Packets;
using DKU_Server.Worlds;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.server;

namespace DKU_Server
{
    public class NetworkManager
    {
        static NetworkManager instance;
        public static NetworkManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new NetworkManager();
                return instance;
            }
        }

        // db 인터페이스
        public IDatabaseManager m_database_manager;
        // 패킷 조립
        public GamePacketHandler m_game_packet_handler;

        // before login
        public Dictionary<long, UserToken> m_waiting_list;
        static long m_waiting_id = 0;
        Stack<long> m_waiting_id_pool;


        public void Init()
        {
            //m_database_manager = new MemoryDatabase();  // 메모리 저장, 휘발성
            m_database_manager = new MySqlDatabase();
            m_database_manager.Init();
            m_game_packet_handler = new GamePacketHandler();

            m_waiting_list = new Dictionary<long, UserToken>();
            m_waiting_id_pool = new Stack<long>();

        }

        public void onNewClient(Socket client_socket, SocketAsyncEventArgs args)
        {
            // UserToken은 유저가 연결되었을 때 해당 유저의 소켓을 저장하고,
            // 메시지를 주고받을 때 사용하는 기능들을 담고 있다.
            UserToken token = new UserToken();
            token.Init();


            // UserToken을 set한다.
            //token.User = user;
            client_socket.NoDelay = true;
            client_socket.ReceiveTimeout = 60 * 1000;
            client_socket.SendTimeout = 60 * 1000;
            token.m_socket = client_socket;
            token.StartRecv();

            long gen_id = GetWaitingId();
            m_waiting_list.Add(gen_id, token);

            S_WaitingIdRes res = new S_WaitingIdRes();
            res.waiting_id = gen_id;
            byte[] serial = res.Serialize();

            Packet packet = new Packet(PacketType.S_WaitingIdRes, serial, serial.Length);
            token.Send(packet);
        }

        public long GetWaitingId()
        {
            lock (m_waiting_id_pool)
            {
                if (m_waiting_id_pool.Count > 0)
                    return m_waiting_id_pool.Pop();
            }
            return m_waiting_id++;
        }
        public void ReturnWaitingId(long id)
        {
            lock(m_waiting_list)
            {
                if(m_waiting_list.ContainsKey(id))
                    m_waiting_list.Remove(id);
            }
            lock (m_waiting_id_pool)
            {
                m_waiting_id_pool.Push(id);
            }
        }
    }
}
