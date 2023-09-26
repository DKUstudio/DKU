using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using Sirenix.OdinInspector;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager instance;
    public static NetworkManager Instance
    {
        get
        {
            if (instance == null)
                instance = new NetworkManager();
            return instance;
        }
    }

    [ShowInInspector]
    private Connections connections;
    public Connections Connections => connections;

    [ShowInInspector]
    private MemberService memberService;
    public MemberService MemberService => memberService;

    private void Awake()
    {
        connections = new Connections();
        memberService = new MemberService();

        Init();
    }

    void Init()
    {
        connections.Init(onConnectionCompleted);
        connections.Connect();
    }

    void onConnectionCompleted(bool val)
    {
        if (val == false)
        {
            Debug.Log("[Connections] Server Connection Failed...");
        }
    }
}
