using System.Collections;
using System.Collections.Generic;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using UnityEngine;

public class PlayerGPS : MonoBehaviour
{
    private Vector3 lastPos = Vector3.zero;
    private Vector3 lastRot = Vector3.zero;
    private void Update()
    {
        if (lastPos != transform.position || lastRot != transform.rotation.eulerAngles)
        {
            lastPos = transform.position;
            lastRot = transform.rotation.eulerAngles;

            C_PlayerPosReq req = new C_PlayerPosReq();
            req.pos = new JVector3(transform.position);
            req.rot = new JVector3(transform.rotation.eulerAngles);
            byte[] body = req.Serialize();

            Packet packet = new Packet(PacketType.C_PlayerPosReq, body, body.Length);
            NetworkManager.Instance.Connections.Send(packet);
        }
    }
}
