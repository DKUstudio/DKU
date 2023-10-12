using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DKU_LoginQueue
{
    public class SocketAsyncEventArgsPool
    {
        private static SocketAsyncEventArgsPool? instance;
        public static SocketAsyncEventArgsPool Instance
        {
            get
            {
                if(instance == null)
                    instance = new SocketAsyncEventArgsPool();
                return instance;
            }
        }
    
        Stack<SocketAsyncEventArgs> pool;

        public SocketAsyncEventArgsPool()
        {
            pool = new Stack<SocketAsyncEventArgs>();
        }

        public SocketAsyncEventArgs Pop()
        {
            if(pool.Count > 0)
            {
                return pool.Pop();
            }
            return new SocketAsyncEventArgs();
        }

        public void Push(SocketAsyncEventArgs args)
        {
            if (args.BufferList != null)
                args.BufferList = null;
            args.SetBuffer(null, 0, 0);
            args.UserToken = null;
            args.RemoteEndPoint = null;

            pool.Push(args);
        }
    }
}
