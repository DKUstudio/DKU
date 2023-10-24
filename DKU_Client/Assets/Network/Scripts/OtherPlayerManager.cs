using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using System.Text;

public class OtherPlayerManager : MonoBehaviour
{
    [ShowInInspector]
    [ReadOnly]
    public Dictionary<long, OtherPlayer> others = new Dictionary<long, OtherPlayer>();

    private WorldManager world_manager;
    public void SetWorldManager(WorldManager v_world_manager)
    {
        world_manager = v_world_manager;
    }

    public void Init(List<UserData> v_ulist)
    {
        //Debug.Log(v_ulist.Count);
        StringBuilder sb = new StringBuilder();
        sb.Append(v_ulist.Count + "\n");
        foreach (var val in v_ulist)
        {
            Debug.Log(val.uid + val.nickname);
            sb.Append(val.uid + " " + val.nickname + "\n");
            if (others.ContainsKey(val.uid) == true)
                continue;
            OtherPlayer op = Instantiate(Resources.Load<OtherPlayer>("OtherPlayer"), this.transform, true);
            others.Add(val.uid, op);
        }
        Debug.Log(sb.ToString());
    }

    public void ControlUserTransform(long uid, JVector3 pos, JVector3 rot)
    {
        bool find_user = others.TryGetValue(uid, out var oplayer);
        if (find_user == false)
        {
            OtherPlayer op = Instantiate(Resources.Load<OtherPlayer>("OtherPlayer"), this.transform, true);
            others.Add(uid, op);

            oplayer = others[uid];
        }


        Vector3 npos = new Vector3(pos.x, pos.y, pos.z);
        Vector3 nrot = new Vector3(rot.x, rot.y, rot.z);

        oplayer.transform.position = npos;
        oplayer.transform.rotation = Quaternion.Euler(nrot);
    }
}