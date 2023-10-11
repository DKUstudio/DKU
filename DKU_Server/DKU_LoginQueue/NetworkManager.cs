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
    
        public GamePacketHandler m_game_packet_handler = new GamePacketHandler();

        public void onNewClient(Socket socket, SocketAsyncEventArgs args )
        {

        }
    }
}
