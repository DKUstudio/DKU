using System.Collections;
using System.Collections.Generic;
using DKU_ServerCore.Packets;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    private WorldManager worldManager;
    public WorldManager WorldManager => worldManager;
    public void SetWorldManager(WorldManager v_world_manager)
    {
        worldManager = v_world_manager;
    }

    private Queue<Packet> send_packets = new Queue<Packet>();

    [ShowInInspector]
    public int send_packets_counts => send_packets.Count;

    private IAdaptivePerformance ap = null;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);

#if UNITY_EDITOR
        ap = Holder.Instance;
        ap.Settings.logging = false;
        ap.DevelopmentSettings.Logging = false;
#endif
    }

    private void Update()
    {
        // 뭐 같은 프레임 내에서 같은 동작을 하는 패킷이 들어오면 무시하는..? 그런 기능 넣을 수도 있긴 한데...
        if (NetworkManager.Instance.Connections == null)
        {
            Debug.Log("[Connections] null");
            return;
        }
        //Debug.LogWarning($"[GameManager] sent {send_packets.Count} packets");
        while (send_packets.Count > 0)
        {
            //Debug.Log("[Send] packet");
            NetworkManager.Instance.Connections.Send(send_packets.Peek());
            send_packets.Dequeue();
        }
    }

    public void PushSendPacket(Packet packet)
    {
        //Debug.Log("[Send] add packet");
        send_packets.Enqueue(packet);
    }
}
