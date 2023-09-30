using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore.Packets
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)] // pack=1: 1byte 단위로 데이터 크기를 맞춤
    public class ChatData
    {
        /// <summary>
        /// 발신자 uid
        /// </summary>
        public long sender_uid;

        /// <summary>
        /// 채팅을 수신할 타겟 enum
        /// </summary>
        public short recv_target_group;

        /// <summary>
        /// 귓속말 시, 수신자 uid
        /// </summary>
        public long recver_uid;

        /// <summary>
        /// 메세지 string
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CommonDefine.MAX_PACKET_STRING_LENGTH)]
        public string message;

        /// <summary>
        /// 링크된 아이템의 번호
        /// </summary>
        public short link_item;

        /// <summary>
        /// 링크된 이모티콘의 번호
        /// </summary>
        public short link_emoticon;

        public ChatData()
        {
            recv_target_group = 0;
            link_item = -1;
            link_emoticon = -1;
        }
    }

    public enum ChatRecvType
    {
        To_All,
        To_Local,
        To_Whisper,
    }
}
