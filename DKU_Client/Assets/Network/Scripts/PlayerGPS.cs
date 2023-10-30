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

    private void Start()
    {
        StartCoroutine(MoveCo());
    }
    C_PlayerPosReq req = new C_PlayerPosReq();
    IEnumerator MoveCo()    // 위치 변화를 감지하면 자동으로 서버로 패킷 쏴줌
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            if (NetworkManager.Instance == null)
            {
                Debug.Log("network manager is null");
            }
            if (NetworkManager.Instance.IS_LOGGED_IN == false)
                continue;

            if (lastPos != transform.position || lastRot != transform.rotation.eulerAngles)
            {
                lastPos = transform.position;
                lastRot = transform.rotation.eulerAngles;

                req.uid = NetworkManager.Instance.UDATA.uid;
                req.pos = new JVector3(transform.position.x, transform.position.y, transform.position.z);
                req.rot = new JVector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                byte[] body = req.Serialize();

                Packet packet = new Packet(PacketType.C_UserPosReq, body, body.Length);
                if (GameManager.Instance == null)
                {
                    Debug.Log("game manager is null");
                }
                GameManager.Instance.PushSendPacket(packet);
            }
        }
    }
}
