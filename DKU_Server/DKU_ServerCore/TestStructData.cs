using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore
{

    [Serializable]
    public enum TestType
    {
        TEST_TYPE_NONE = -1,

        TEST_TYPE_1,
        TEST_TYPE_2,
        TEST_TYPE_3,

        TEST_TYPE_COUNT
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack =1, CharSet=CharSet.Unicode)]
    public struct TestStructData
    {
        public TestType m_test_enum;

        public long m_long;
        public float m_float;
        public bool m_bool;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string m_name;

        public TestStructData(long v_long,
            string v_name,
            float v_float,
            bool v_bool,
            TestType v_test_enum)
        {
            m_long = v_long;
            m_name = v_name;
            m_float = v_float;
            m_bool = v_bool;
            m_test_enum = v_test_enum;
        }
    }
}
