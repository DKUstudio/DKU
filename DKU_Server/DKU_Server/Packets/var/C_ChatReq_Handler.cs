
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKU_Server.Worlds;
using DKU_Server.Connections;

namespace DKU_Server.Packets.var
{
    public class C_ChatReq_Handler
    {
        public static void Method(SPacket packet)
        {
            C_ChatReq req = Data<C_ChatReq>.Deserialize(packet.m_data);

            switch((ChatRecvType)req.chatData.recv_target_group)
            {
                case ChatRecvType.To_All:
                    NetworkManager.Instance.world.ShootGlobalChat(req.chatData);
                    break;

                case ChatRecvType.To_Local:
                    NetworkManager.Instance.world.ShootLocalChat(req.chatData);
                    break;

                case ChatRecvType.To_Whisper:
                    NetworkManager.Instance.world.ShootWhisperChat(req.chatData);
                    break;

                default:
                    Console.WriteLine("[Chat] Invalid recv target");
                    break;
            }
        }
    }
}
