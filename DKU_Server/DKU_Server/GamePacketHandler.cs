using DKU_Server.Connections;
using DKU_Server.Connections.Tokens;
using DKU_Server.Worlds;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server
{
    public class GamePacketHandler
    {
        public void ParsePacket(Packet packet)
        {
            switch ((PacketType)packet.m_type)
            {
                case PacketType.C_RegisterReq:
                    C_RegisterReq_Handler(packet);
                    break;

                case PacketType.C_LoginReq: 
                    C_LoginReq_Handler(packet);
                    break;

                case PacketType.C_LogoutReq:
                    C_LogoutReq_Handler(packet);
                    break;

                case PacketType.C_GlobalChatReq:
                    C_GlobalChatReq_Handler(packet);
                    break;
            }
        }

        void C_RegisterReq_Handler(Packet packet)
        {
            C_RegisterReq req = Data<C_RegisterReq>.Deserialize(packet.m_data);
            bool isTrue = NetworkManager.Instance.m_waiting_list.TryGetValue(req.accept_id, out UserToken token);
            if (isTrue == false)
                return;

            bool success = NetworkManager.Instance.m_database_manager.Register(req.id, req.pw, req.nickname);

            S_RegisterRes res = new S_RegisterRes();
            res.success = success;
            byte[] serial = res.Serialize();

            Packet pkt = new Packet();
            pkt.SetData(PacketType.S_RegisterRes, serial, serial.Length);
            token.Send(pkt);
        }

        void C_LoginReq_Handler(Packet packet)
        {
            // 로그인 성공시, 대기 리스트에서 월드방으로 이동
            C_LoginReq req = Data<C_LoginReq>.Deserialize(packet.m_data);
            bool isTrue = NetworkManager.Instance.m_waiting_list.TryGetValue(req.accept_id, out UserToken token);
            if (isTrue == false)
                return;

            UserData udata = NetworkManager.Instance.m_database_manager.Login(req.id, req.pw);
            S_LoginRes res = new S_LoginRes();
            if(udata != null)
            {
                // 성공
                res.success = true;
                res.udata = udata;

                LoginData ldata = new LoginData(token, udata);
                NetworkManager.Instance.ReturnAcceptId(req.accept_id);
                TheWorld.Instance.AddUser(ldata);
            }
            else
            {
                // 실패
                res.success = false;
            }
            byte[] serial = res.Serialize();

            Packet pkt = new Packet(PacketType.S_LoginRes, serial, serial.Length);
            token.Send(pkt);
        }

        void C_GlobalChatReq_Handler(Packet packet)
        {
            C_GlobalChatReq req = Data<C_GlobalChatReq>.Deserialize(packet.m_data);
            TheWorld.Instance.GlobalChat(req);
/*
            Console.WriteLine("recved: " + req.chat_message);
            GlobalChatRes res = new GlobalChatRes();
            res.chat_message = req.chat_message;
            byte[] serial = res.Serialize();

            Packet resPkt = new Packet();
            resPkt.SetData(serial, serial.Length);
            resPkt.m_type = (int)PacketType.S_GlobalChatRes;*/

            
        }

        void C_LogoutReq_Handler(Packet packet)
        {
            C_LogoutReq req = Data<C_LogoutReq>.Deserialize(packet.m_data);
        }

    }
}
