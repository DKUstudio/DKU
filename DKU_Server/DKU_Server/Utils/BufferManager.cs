﻿using DKU_ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Utils
{
    // SocketAsyncEventArgs 들이 나누어 가질 수 있는 하나의 큰 바이트 배열을 만듦
    // 이렇게 하면 heap 메모리의 단편화 없이 깔끔하게 관리 가능함
    // 대신 최대 동접자로 설정한 수 만큼의 유저만 수용할 수 있음...
    public class BufferManager
    {
        static BufferManager instance;
        public static BufferManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new BufferManager();
                return instance;
            }
        }

        int m_numBytes;                 // 총 바이트 수
        byte[] m_buffer;                // 하나의 큰 바이트 배열
        Stack<int> m_freeIndexPool;
        int m_currentIndex;             // 
        int m_bufferSize;               // 나눠가질 버퍼의 단위 크기
        public int GetRemained()
        {
            //LogManager.Log($"numBytes: {m_numBytes}, m_curIdx: {m_currentIndex}, m_bufSize: {m_bufferSize}, m_freePool: {m_freeIndexPool.Count}");
            return ((m_numBytes - m_currentIndex) / m_bufferSize) / 2 + m_freeIndexPool.Count / 2;
        }

        public BufferManager()
        {
            // (recv, send) 2개
            m_bufferSize = CommonDefine.SOCKET_BUFFER_SIZE;
            m_numBytes = CommonDefine.MAX_CONNECTION * 2 * CommonDefine.SOCKET_BUFFER_SIZE;
            m_currentIndex = 0;
            m_freeIndexPool = new Stack<int>();

            m_buffer = new byte[m_numBytes];
        }

        // <returns> 정상적으로 버퍼를 받아가면 true, 아니면 false </returns>
        public bool SetBuffer(SocketAsyncEventArgs args)
        {
            if (m_freeIndexPool.Count > 0)   // 사용하고 해제된 오프셋이 있을 때
            {
                args.SetBuffer(m_buffer, m_freeIndexPool.Pop(), m_bufferSize);
            }
            else
            {
                if (m_numBytes - m_bufferSize < m_currentIndex)    // 한번씩은 이미 다 사용했을 때
                {
                    return false;
                }
                args.SetBuffer(m_buffer, m_currentIndex, m_bufferSize);
                m_currentIndex += m_bufferSize;
            }
            return true;
        }

        // 버퍼를 사용해제하고 사용가능한 스택에 추가해줌 (재활용)
        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            if (args == null)
                return;
            m_freeIndexPool.Push(args.Offset);
            try
            {
                SocketAsyncEventArgsPool.Instance.Push(args);
            }
            catch (Exception e)
            {
                LogManager.Log("[SocketAsyncEventArgs] Disposed: " + e.Message);
                args.Dispose();
            }
        }
    }
}
