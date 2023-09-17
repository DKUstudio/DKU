using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClient
{
    public class MessageResolver
    {

        public void onRecv(byte[] v_buffer, int v_offset, int v_bytesTransferred, Action<object, SocketAsyncEventArgs> onComplete) 
        {
        }

    }
}
