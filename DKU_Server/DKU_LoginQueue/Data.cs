using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DKU_LoginQueue
{
    // 직렬화할 수 있도록
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]     // Pack=1: 1byte 단위로 데이터의 크기를 맞춤
    public class Data<T> where T : class
    {
        public byte[] Serialize()
        {
            var size = Marshal.SizeOf(typeof(T));
            var array = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(this, ptr, true);
            Marshal.Copy(ptr, array, 0, size);
            Marshal.FreeHGlobal(ptr);

            return array;
        }

        public static T Deserialize(byte[] array)
        {
            var size = Marshal.SizeOf(typeof(T));
            var ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(array, 0, ptr, size);

            var s = (T)Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);

            return s;
        }
    }
}
