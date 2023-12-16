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

    [ShowInInspector]
    [Sirenix.OdinInspector.ReadOnly]
    private bool is_waiting = false;
    public bool IS_WAITING => is_waiting;
    [ShowInInspector]
    [Sirenix.OdinInspector.ReadOnly]
    private long wid = -1;
    public long WID => wid;
    [ShowInInspector]
    [Sirenix.OdinInspector.ReadOnly]
    private bool is_logged_in = false;
    public bool IS_LOGGED_IN => is_logged_in;
    [ShowInInspector]
    [Sirenix.OdinInspector.ReadOnly]
    private UserData udata;
    public UserData UDATA => udata;
    public short current_world_block_number = 0;

    public void SetIsWaiting(bool val)
    {
        is_waiting = val;
    }
    public void SetWid(long v_wid)
    {
        wid = v_wid;
    }
    public void SetIsLoggedIn(bool val)
    {
        is_logged_in = val;
    }
    public void SetUdata(UserData v_udata)
    {
        udata = v_udata;
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);

        connections = this.AddComponent<Connections>();
        connections.Init();
        connections.Connect();
    }

    [Button]
    private void Add_Test_Components()
    {
        this.AddComponent<MemberService>();
        this.AddComponent<ChatService>();
    }

    void CloseConnection()
    {
        connections.CloseSocketConnection();
        connections = null;
        Destroy(this.GetComponent<Connections>());
        Debug.Log("[Connections] removed connection");
    }

    public void RestartConnection(string ip_address)
    {
        StartCoroutine(RestartConnectionCoroutine(ip_address));
    }

    public IEnumerator RestartConnectionCoroutine(string ip_address)
    {
        CloseConnection();

        while (TryGetComponent<Connections>(out Connections c))
        {
            yield return null;
        }
        this.AddComponent<Connections>();
        connections = this.GetComponent<Connections>();
        connections.Init();

        connections.Connect(ip_address);
    }

}
