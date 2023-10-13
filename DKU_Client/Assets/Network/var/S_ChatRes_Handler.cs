
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using UnityEngine;

// 채팅 정보 수신
// from C_ChatReq
public class S_ChatRes_Handler
{
    public static void Method(Packet packet)
    {
        S_ChatRes res = Data<S_ChatRes>.Deserialize(packet.m_data);
        string opt = "";
        switch ((ChatRecvType)res.chatData.recv_target_group)
        {
            case ChatRecvType.To_All:
                opt += "<color=yellow>[To_All]</color> ";
                break;

            case ChatRecvType.To_Local:
                opt += "<color=yellow>[To_Local]</color> ";
                break;

            case ChatRecvType.To_Whisper:
                opt += "<color=yellow>[To_Whisper]</color> ";
                break;

            default:
                break;
        }

        opt += res.chatData.message;
        Debug.Log(opt);
    }
}
