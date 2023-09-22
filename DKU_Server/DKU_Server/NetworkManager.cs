using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DKU_Server.Connections.Tokens;
using DKU_Server.Users;
using DKU_ServerCore.Packets;

namespace DKU_Server
{
    public class NetworkManager
    {
        static NetworkManager instance;
        public static NetworkManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new NetworkManager();
                return instance;
            }
        }

        public static short sampleid = 0;
        public Dictionary<short, UserToken> tokens = new Dictionary<short, UserToken>();

        public void onNewClient(Socket client_socket, object event_args)
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


            // welcome data
            byte[] data = Encoding.Unicode.GetBytes("Welcome to DKU server...");
            Packet packet = new Packet();
            packet.SetData(data, data.Length);
            token.Send(packet);

            tokens.Add(sampleid++, token);

            // User객체는 db에서 가져온 데이터를 저장하는 객체이다. 말 그대로 접속한 유저의 정보를 가지고 있다.
            //UserData user = new UserData(); // 나중에 UserDataPool로 최적화.
            //user.Init(token);
        }

        public void TestPing()
        {
            foreach(var token in tokens)
            {
                byte[] data = Encoding.Unicode.GetBytes("test ping...");
                Packet packet = new Packet();
                packet.SetData(data, data.Length);
                (token.Value as UserToken).Send(packet);
            }
        }
    }
}
