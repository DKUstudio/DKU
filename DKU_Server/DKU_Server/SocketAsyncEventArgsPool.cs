using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server
{
    public class SocketAsyncEventArgsPool
    {
        Stack<SocketAsyncEventArgs> m_pool;
        public int Count => m_pool.Count;

        public SocketAsyncEventArgsPool()
        {
            m_pool = new Stack<SocketAsyncEventArgs>(Constants.MAX_CONNECTION);

            for(int i = 0; i < Constants.MAX_CONNECTION; i++)
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
                if(m_pool.Count >= Constants.MAX_CONNECTION)
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
                return m_pool.Pop();
            }
        }
    }
}
