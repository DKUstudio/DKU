using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server
{
    enum Defines
    {
        HEADERSIZE = sizeof(ushort),
    }

    class MessageResolver
    {

        int _remainBytes;
        int _curPos;
        int _posToRead;
        int _messageSize;
        byte[] _messageBuffer;


        // 소켓 버퍼로부터 데이터를 수신할 때마다 호출됨
        // 패킷이 완성될 때마다 calback을 호출해준다
        // 하나의 패킷을 완성하지 못했다면 버퍼에 보관해 놓은 뒤 다음 수신을 기다린다
        public void OnReceive(byte[] buffer, int offset, int bytesTransferred, Action<byte[]> callback)
        {
            // 이전 행동에서 0이 될때까지 수행하므로 0이 되는것이 보장됨
            _remainBytes = bytesTransferred;

            int src_position = offset;

            // 남은 데이터가 있으면 계속
            while(_remainBytes > 0)
            {
                bool completed = false;

                // 헤더를 읽을 수 있는지 확인
                if(_curPos < (int)Defines.HEADERSIZE)
                {
                    // 목표 지점
                    _posToRead = (int)Defines.HEADERSIZE;

                    completed = ReadUntil(buffer, ref src_position, offset, bytesTransferred);
                    if(completed == false)
                    {
                        // 아직 다 못읽었으므로 다음 receive를 기다리도록 함
                        return;
                    }

                    // 헤더 읽기 성공, 바디 사이즈를 계산함
                    _messageSize = GetBodySize();

                    // 다음 목표 지점
                    _posToRead = (int)Defines.HEADERSIZE + _messageSize;
                }

                // 바디 읽기
                completed = ReadUntil(buffer, ref src_position, offset, bytesTransferred);

                if(completed == true)
                {
                    // 패킷 완성
                    callback.Invoke(_messageBuffer);

                    ClearBuffer();
                }
            }
        }

        bool ReadUntil(byte[] buffer, ref int src_pos, int offset, int transferred)
        {
            if(_curPos >= offset + transferred)
            {
                // 들어온 데이터만큼 모두 읽었던 상태임
                return false;
            }

            // 읽어와야 할 바이트
            // 목표위치 - 현재위치
            int copy_size = _posToRead - _curPos;

            // 남은 데이터가 적다면 가능한 만큼만 복사
            if(_remainBytes < copy_size)
            {
                copy_size = _remainBytes;
            }

            // 버퍼 복사
            Array.Copy(buffer, src_pos, _messageBuffer, _curPos, copy_size);

            // 원본 버퍼 포지션 이동
            src_pos += copy_size;

            // 복사된 버퍼 포지션 이동
            _curPos += copy_size;

            // 가용 바이트 수 갱신
            _remainBytes -= copy_size;

            if(_curPos < _posToRead)
            {
                return false;
            }
            return true;
        }

        int GetBodySize()
        {
            return BitConverter.ToInt16(_messageBuffer, 0);
        }

        void ClearBuffer()
        {
            Array.Clear(_messageBuffer, 0, _messageBuffer.Length);

            _curPos = 0;
            _messageSize = 0;
        }
    }
}
