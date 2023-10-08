
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKU_Server.Worlds;

namespace DKU_Server.Packets.var
{
    public class C_ChatReq_Handler
    {
        public static void Method(Packet packet)
        {
            C_ChatReq req = Data<C_ChatReq>.Deserialize(packet.m_data);

            switch((ChatRecvType)req.chatData.recv_target_group)
            {
                case ChatRecvType.To_All:
                    TheWorld.Instance.ShootGlobalChat(req.chatData);
                    break;

                case ChatRecvType.To_Local:
                    TheWorld.Instance.ShootLocalChat(req.chatData);
                    break;

                case ChatRecvType.To_Whisper:
                    TheWorld.Instance.ShootWhisperChat(req.chatData);
                    break;

                default:
                    Console.WriteLine("[Chat] Invalid recv target");
                    break;
            }
        }
    }
}
