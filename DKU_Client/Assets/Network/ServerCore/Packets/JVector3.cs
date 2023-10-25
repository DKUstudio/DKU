using System;
using System.Collections.Generic;
using System.Linq;
//using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

#if DKU_UNITY
using UnityEngine;
#endif

namespace DKU_ServerCore.Packets
{
    // 직렬화할 수 있도록
    [Serializable]
    //[StructLayout(LayoutKind.Sequential, Pack = 1)]     // Pack=1: 1byte 단위로 데이터의 크기를 맞춤
    public class JVector3
    {
        //[MarshalAs(UnmanagedType.R4)]
        public float x;
        //[MarshalAs(UnmanagedType.R4)]
        public float y;
        //[MarshalAs(UnmanagedType.R4)]
        public float z;

        public JVector3()
        {
            x = y = z = 0f;
        }

        public JVector3(float val)
        {
            x = y = z = val;
        }

        public JVector3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

#if DKU_UNITY
        public JVector3(UnityEngine.Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }
#endif
    }
}
