using DKU_ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClient
{
    public class SocketAsyncEventArgsPool
    {
        static SocketAsyncEventArgsPool instance;
        public static SocketAsyncEventArgsPool Instance
        {
            get
            {
                if (instance == null)
                    instance = new SocketAsyncEventArgsPool();
                return instance;
            }
        }

        Stack<SocketAsyncEventArgs> m_pool;
        public int Count => m_pool.Count;
        public SocketAsyncEventArgsPool()
        {
            m_pool = new Stack<SocketAsyncEventArgs>();
        }


        public void Push(SocketAsyncEventArgs item)
        {
            if (item == null) { throw new ArgumentNullException("items added to a SocketAsyncEventArgsPool cannot be null"); }
            m_pool.Push(item);
        }

        public SocketAsyncEventArgs Pop()
        {
            if (m_pool.Count > 0)
                return m_pool.Pop();
            else
                return new SocketAsyncEventArgs();
        }
    }
}
