
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;
using UnityEngine.SceneManagement;
using DKU_ServerCore;

public class S_WorldChangeAvailRes_Handler
{
    public static void Method(Packet packet)
    {
        S_WorldChangeAvailRes res = Data<S_WorldChangeAvailRes>.Deserialize(packet.m_data);

        // 입장 가능
        if (res.success == 0)
        {
            NetworkManager.Instance.current_world_block_number = res.room_number;
            switch ((CommonDefine.WorldBlockType)res.room_number)
            {
                case CommonDefine.WorldBlockType.Dankook_University:
                    SceneManager.LoadScene("MainMap");
                    break;
                case CommonDefine.WorldBlockType.Suika_Game:
                    SceneManager.LoadScene("AnimalGame");
                    break;
                case CommonDefine.WorldBlockType.OX_quiz:
                    SceneManager.LoadScene("leftright");
                    break;
                case CommonDefine.WorldBlockType.Hexagon:
                    SceneManager.LoadScene("FALLGAME");
                    break;
            }
            //SceneManager.LoadScene("leftright");
        }
        // 입장 실패
        else if (res.success == 1)
        {
            Debug.Log((CommonDefine.WorldBlockType)res.room_number + "방 정원 초과로 입장 불가");
        }
    }
}
