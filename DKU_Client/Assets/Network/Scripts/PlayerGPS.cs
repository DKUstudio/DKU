using System.Collections;
using System.Collections.Generic;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerGPS : MonoBehaviour
{
    [ShowInInspector]
    [ReadOnly]
    private Vector3 lastPos = Vector3.zero;
    [ShowInInspector]
    [ReadOnly]
    private Vector3 lastRot = Vector3.zero;

    private void Update()   // 위치 변화를 감지하면 자동으로 서버로 패킷 쏴줌
    {
        if (NetworkManager.Instance.IS_LOGGED_IN == false)
            return;

        if (lastPos != transform.position || lastRot != transform.rotation.eulerAngles)
        {
            lastPos = transform.position;
            lastRot = transform.rotation.eulerAngles;

            C_PlayerPosReq req = new C_PlayerPosReq();
            req.uid = NetworkManager.Instance.UDATA.uid;
            req.pos = new JVector3(transform.position);
            req.rot = new JVector3(transform.rotation.eulerAngles);
            byte[] body = req.Serialize();

            Packet packet = new Packet(PacketType.C_PlayerPosReq, body, body.Length);
            //NetworkManager.Instance.Connections.Send(packet);
            GameManager.Instance.PushSendPacket(packet);
        }
    }
}
