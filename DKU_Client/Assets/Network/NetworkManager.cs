using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager instance;
    public static NetworkManager Instance => instance;

    private Connections connections;
    public Connections Connections => connections;

    private void Awake()
    {
        instance = this;

        connections = GetComponent<Connections>();

        connections.Init();
        connections.Connect();
    }

    [Button]
    private void Add_Component()
    {
        this.AddComponent<Connections>();
        this.AddComponent<MemberService>();
        this.AddComponent<ChatService>();
    }

}
