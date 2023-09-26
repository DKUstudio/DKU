
using DKU_Server.Connections.Tokens;
using DKU_Server.Connections;
using DKU_Server.Worlds;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Packets.var
{
    public class C_LoginReq_Handler
    {
        public static void Method(Packet packet)
        {
            // 로그인 성공시, 대기 리스트에서 월드방으로 이동
            C_LoginReq req = Data<C_LoginReq>.Deserialize(packet.m_data);
            Console.WriteLine(req.accept_id + " login req");
            bool isTrue = NetworkManager.Instance.m_waiting_list.TryGetValue(req.accept_id, out UserToken token);
            if (isTrue == false)
                return;

            UserData udata = NetworkManager.Instance.m_database_manager.Login(req.id, req.pw);
            S_LoginRes res = new S_LoginRes();
            if (udata != null)
            {
                // 성공
                res.success = true;
                res.udata = udata;

                LoginData ldata = new LoginData(token, udata);
                NetworkManager.Instance.ReturnWaitingId(req.accept_id);
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
    }
}
