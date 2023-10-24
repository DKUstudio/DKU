using System.Collections;
using System.Collections.Generic;
using DKU_ServerCore.Packets;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    private Queue<Packet> send_packets = new Queue<Packet>();
    [ShowInInspector]
    public int send_packets_counts => send_packets.Count;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        // 뭐 같은 프레임 내에서 같은 동작을 하는 패킷이 들어오면 무시하는..? 그런 기능 넣을 수도 있긴 한데...
        if (NetworkManager.Instance.Connections == null)
            return;
        //Debug.LogWarning($"[GameManager] sent {send_packets.Count} packets");
        while (send_packets.Count > 0)
        {
            NetworkManager.Instance.Connections.Send(send_packets.Peek());
            send_packets.Dequeue();
        }
    }

    public void PushSendPacket(Packet packet)
    {
        send_packets.Enqueue(packet);
    }
}
