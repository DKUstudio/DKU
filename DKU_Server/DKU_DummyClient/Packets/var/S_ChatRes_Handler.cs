
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClient.Packets.var
{
    public class S_ChatRes_Handler
    {
        public static void Method(Packet packet)
        {
            S_ChatRes res = Data<S_ChatRes>.Deserialize(packet.m_data);
            switch ((ChatRecvType)res.chatData.recv_target_group)
            {
                case ChatRecvType.To_All:
                    Console.WriteLine("[To_All]");
                    break;

                case ChatRecvType.To_Local:
                    Console.WriteLine("[To_Local]");
                    break;

                case ChatRecvType.To_Whisper:
                    Console.WriteLine("[To_Whisper]");
                    break;

                default:
                    Console.WriteLine("[Chat] Invalid recv target");
                    break;
            }
            Console.WriteLine(res.chatData.message);
        }
    }
}
