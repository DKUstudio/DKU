
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClient.Packets.var
{
    public class S_RegisterRes_Handler
    {
        public static void Method(Packet packet)
        {
            S_RegisterRes res = Data<S_RegisterRes>.Deserialize(packet.m_data);
            if (res.success)
            {
                // ����
                Console.WriteLine("ȸ������ ����");
            }
            else
            {
                // ����
                Console.WriteLine("ȸ������ ����");
            }
        }
    }
}
