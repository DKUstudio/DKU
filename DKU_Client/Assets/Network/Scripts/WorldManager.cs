using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Required]
    public CommonDefine.WorldBlockType w_type;

    private OtherPlayerManager otherPlayerManager;
    public OtherPlayerManager OtherPlayerManager => otherPlayerManager;

    private void Start()    // 시작할 때 월드 세팅 담당
    {
        GameManager.Instance.SetWorldManager(this); // 싱글톤

        otherPlayerManager = this.GetComponent<OtherPlayerManager>(); // 다른 유저들 관리 매니저
        otherPlayerManager.SetWorldManager(this); // 의존성 주입

        C_GetWorldUsersDataReq req = new C_GetWorldUsersDataReq(); // 해당 월드의 다른 유저들 정보를 얻어옴
        req.uid = NetworkManager.Instance.UDATA.uid;
        byte[] body = req.Serialize();
        Packet packet = new Packet(PacketType.C_GetWorldUsersDataReq, body, body.Length);

        Task t = new Task(() => SendWorldUsersReq(packet));
        t.Start();
        Debug.Log("[New world] init");
    }

    /// <summary>
    /// Connection이 완전히 연결되기 전까지 기다리기 위함
    /// </summary>
    /// <param name="packet"></param>
    void SendWorldUsersReq(Packet packet)
    {
        while (NetworkManager.Instance.Connections == null)
        {
        }
        NetworkManager.Instance.Connections.Send(packet);
    }

}
