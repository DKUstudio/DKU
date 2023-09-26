using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using Sirenix.OdinInspector;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager instance;
    public static NetworkManager Instance => instance;

    private Connections connections;
    public Connections Connections => connections;

    // private MemberService memberService;
    // public MemberService MemberService => memberService;

    // private ChatService chatService;
    // public ChatService ChatService => chatService;

    private void Awake()
    {
        instance = this;

        connections = GetComponent<Connections>();

        connections.Init();
        connections.Connect();
    }

}
