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

    [Sirenix.OdinInspector.ReadOnly]
    private bool is_waiting = false;
    public bool IS_WAITING => is_waiting;
    [Sirenix.OdinInspector.ReadOnly]
    private long wid = 0;
    public long WID => wid;
    private int sid = 0;
    public int SID => sid;
    [Sirenix.OdinInspector.ReadOnly]
    private bool is_logged_in = false;
    public bool IS_LOGGED_IN => is_logged_in;
    [Sirenix.OdinInspector.ReadOnly]
    private UserData udata;
    public UserData UDATA => udata;

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
    public void SetSid(int v_sid)
    {
        sid = v_sid;
    }

    private void Awake()
    {
        instance = this;

        connections = GetComponent<Connections>();
        DontDestroyOnLoad(this);
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

    void CloseConnection()
    {
        connections.CloseSocketConnection();
        connections = null;
        Destroy(this.GetComponent<Connections>());
        Debug.Log("[Connections] removed connection");
    }

    public void StartConnection(string ip_address)
    {
        // CloseConnection();
        // this.AddComponent<Connections>();
        // connections = this.GetComponent<Connections>();
        // connections.Init();

        // connections.Connect(ip_address);
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
