using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore
{
    public class SocketAsyncEventArgsPool
    {
        static SocketAsyncEventArgsPool instance;
        public static SocketAsyncEventArgsPool Instance
        {
            get { 
                if (instance == null)
                    instance = new SocketAsyncEventArgsPool();
                return instance;
            }
        }

        Stack<SocketAsyncEventArgs> m_pool;
        public int Count => m_pool.Count;

        public SocketAsyncEventArgsPool()
        {
            m_pool = new Stack<SocketAsyncEventArgs>(CommonDefine.MAX_CONNECTION);

            for (int i = 0; i < CommonDefine.MAX_CONNECTION; i++)
            {
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                m_pool.Push(args);
            }
        }

        public void Push(SocketAsyncEventArgs item)
        {
            if (item == null) { throw new ArgumentNullException("items added to a SocketAsyncEventArgsPool cannot be null"); }
            lock (m_pool)
            {
                if (m_pool.Count >= CommonDefine.MAX_CONNECTION)
                {
                    item.Dispose();
                    return;
                }
                m_pool.Push(item);
            }
        }

        public SocketAsyncEventArgs Pop()
        {
            lock (m_pool)
            {
                if(m_pool.Count > 0)
                    return m_pool.Pop();
                else
                {
                    SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                    return args;
                }
            }
        }
    }
}
