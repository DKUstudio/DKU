using DKU_ServerCore;
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
        private static NetworkManager? instance;
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
        public DatabaseManager m_database_manager = new DatabaseManager();

        public List<LoginData> m_login_accept_list = new List<LoginData>();

        public NetworkManager()
        {
            m_database_manager.Init();
            Task login_accept = new Task(LoginAccept);
            login_accept.Start();
        }

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
            LogManager.Log($"[waitingID] gave new wid {wid}");
            m_wid_list.Add(wid, token);

            Q_YourWidRes res = new Q_YourWidRes();
            res.wid = wid;
            byte[] body = res.Serialize();

            Packet packet = new Packet(PacketType.Q_YourWidRes, body, body.Length);
            token.Send(packet);

            LogManager.Log($"[onNewClient] came {client_socket.RemoteEndPoint.ToString()}");
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
            if (m_wid_list.ContainsKey(wid))
                m_wid_list.Remove(wid);

            m_wid_pool.Push(wid);
        }

        public void NewGameServer(long wid)
        {
            if (m_wid_list.ContainsKey(wid))
            {
                m_game_server = m_wid_list[wid];
                m_wid_list.Remove(wid);
                Returnwid(wid);
            }

        }

        public void PushLoginAcceptList(long v_wid, UserData udata)
        {
            LoginData loginData = new LoginData(m_wid_list[v_wid], udata);
            lock (m_wid_list)
            {
                m_wid_list.Remove(v_wid);
                Returnwid(v_wid);
            }
            m_login_accept_list.Add(loginData);
        }

        void LoginAccept()
        {
            while (true)
            {
                Thread.Sleep(5000);
                if (m_login_accept_list.Count > 0)
                {
                    Q_CurUsersCountReq req = new Q_CurUsersCountReq();
                    req.login_accept_list_count = m_login_accept_list.Count;
                    byte[] body = req.Serialize();

                    Packet packet = new Packet(PacketType.Q_CurUsersCountReq, body, body.Length);
                    if(m_game_server != null)
                    {
                        m_game_server.Send(packet);
                        LogManager.Log("[GameServer] check available seat");
                    }
                    else
                        LogManager.Log("[GameServer] is null");
                }
            }
        }


        Packet? goto_packet;
        Q_WaitForLoginRes wait_for_res = new Q_WaitForLoginRes();
        public void LoginUsers(int amount)
        {
            if (goto_packet == null)
            {
                Q_GoToGameServerRes goto_game_server = new Q_GoToGameServerRes();
                byte[] goto_body = goto_game_server.Serialize();
                goto_packet = new Packet(PacketType.Q_GoToGameServerRes, goto_body, goto_body.Length);
            }
            lock (m_login_accept_list)
            {
                for (int i = 0; i < Math.Min(amount, m_login_accept_list.Count); i++)
                {
                    LogManager.Log($"[Goto GameServer] hello, {m_login_accept_list.ElementAt(0).UserData.nickname}");
                    if(m_login_accept_list.ElementAt(0).UserToken.m_socket.Connected)
                        m_login_accept_list.ElementAt(0).UserToken.Send(goto_packet);
                    m_login_accept_list.RemoveAt(0);
                }
            }
            // 남은 유저들에게 대기 순번 쏴줌
            for(int i = 0; i < m_login_accept_list.Count; i++)
            {
                wait_for_res.my_waiting_num = i;
                byte[] body = wait_for_res.Serialize();

                Packet wait_packet = new Packet(PacketType.Q_WaitForLoginRes, body, body.Length);
                m_login_accept_list[i].UserToken.Send(wait_packet);
            }
        }
    }
}
