using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using System.Text;
using DG.Tweening;

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
            sb.Append(val.uid + " " + val.nickname + " " + val.charaShift + "\n");
            if (others.ContainsKey(val.uid) == true)
                continue;
            AddUser(val.uid, val);
            // OtherPlayer op = GetOtherPlayerGameObject();
            // op.SetUserData(val);
            // op.CharaChangeTo(val.charaShift);
            // others.Add(val.uid, op);
        }
        Debug.Log(sb.ToString());
    }

    public void ControlUserTransform(long uid, JVector3 pos, JVector3 rot)
    {
        bool find_user = others.TryGetValue(uid, out var oplayer);
        if (find_user == false)
        {
            OtherPlayer op = GetOtherPlayerGameObject();
            others.Add(uid, op);

            oplayer = others[uid];
        }


        Vector3 npos = new Vector3(pos.x, pos.y, pos.z);
        Vector3 nrot = new Vector3(rot.x, rot.y, rot.z);

        oplayer.MoveTo(npos);
        oplayer.RotateTo(nrot);
    }

    public void AddUser(long v_uid, UserData v_udata)
    {
        if (others.ContainsKey(v_uid) == true)
            return;
        OtherPlayer op = GetOtherPlayerGameObject();
        op.SetUserData(v_udata);
        op.CharaChangeTo(v_udata.charaShift);
        others.Add(v_uid, op);
    }

    public void RemoveUser(long v_uid)
    {
        if (others.ContainsKey(v_uid))
        {
            OtherPlayer other = others[v_uid];
            others.Remove(v_uid);
            Destroy(other.gameObject);
        }
    }

    OtherPlayer GetOtherPlayerGameObject()
    {
        return Instantiate(Resources.Load<OtherPlayer>("otherPlayer2"), this.transform, true);
    }

    public void CharaShiftChange(long v_uid, short v_shift)
    {
        if (others.ContainsKey(v_uid) == false)
            return;
        others[v_uid].CharaChangeTo(v_shift);
    }

    public void AnimChange(long v_uid, string v_animName)
    {
        if (others.ContainsKey(v_uid) == false)
            return;
        others[v_uid].AnimationChangeTo(v_animName);
    }
}
