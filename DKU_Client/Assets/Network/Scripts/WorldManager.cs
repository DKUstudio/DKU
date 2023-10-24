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
        GameManager.Instance.SetWorldManager(this);

        otherPlayerManager = this.AddComponent<OtherPlayerManager>();
        otherPlayerManager.SetWorldManager(this);

        C_GetWorldUsersDataReq req = new C_GetWorldUsersDataReq();
        req.uid = NetworkManager.Instance.UDATA.uid;
        byte[] body = req.Serialize();
        Packet packet = new Packet(PacketType.C_GetWorldUsersDataReq, body, body.Length);

        Task t = new Task(() => SendWorldUsersReq(packet));
        t.Start();
        Debug.Log("[New world] init");
    }

    void SendWorldUsersReq(Packet packet)
    {
        while (NetworkManager.Instance.Connections == null)
        {
        }
        NetworkManager.Instance.Connections.Send(packet);
    }

}
