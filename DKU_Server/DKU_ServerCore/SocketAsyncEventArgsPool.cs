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
        Stack<SocketAsyncEventArgs> m_pool;
        public int Count => m_pool.Count;

        public SocketAsyncEventArgsPool(int capacity)
        {
            m_pool = new Stack<SocketAsyncEventArgs>(capacity);
        }

        public void Push(SocketAsyncEventArgs item)
        {
            if(item == null) { throw new ArgumentNullException("items added to a SocketAsyncEventArgsPool cannot be null"); }
            lock(m_pool)
            {
                m_pool.Push(item);
            }
        }

        public SocketAsyncEventArgs Pop()
        {
            lock(m_pool)
            {
                return m_pool.Pop();
            }
        }
    }
}
