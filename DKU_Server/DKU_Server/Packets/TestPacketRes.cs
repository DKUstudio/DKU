using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DKU_ServerCore;

namespace DKU_DummyClinet.Packets
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack =1, CharSet = CharSet.Unicode)]
    public class TestPacketRes : Data<TestPacketRes>
    {
        public bool m_is_success;
        public int m_test_int_value;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CommonDefine.MAX_PACKET_STRING_LENGTH)]
        public string m_message;
    }
}
