using System.Collections;
using System.Collections.Generic;
using DKU_ServerCore.Packets;
using UnityEngine;
using DG.Tweening;


public class OtherPlayer : MonoBehaviour
{
    private UserData udata;
    public UserData UDATA => udata;

    private Tweener pos_tweener;
    private Tweener rot_tweener;

    public void SetUserData(UserData v_udata)
    {
        udata = v_udata;
    }

    public void MoveTo(Vector3 v_pos)
    {
        if (pos_tweener == null)
        {
            pos_tweener = transform.DOMove(v_pos, 0.2f).SetAutoKill(false);
            return;
        }
        pos_tweener.ChangeEndValue(v_pos, true).Restart();
    }

    public void RotateTo(Vector3 v_rot)
    {
        if (rot_tweener == null)
        {
            rot_tweener = transform.DORotate(v_rot, 0.2f).SetAutoKill(false);
            return;
        }
        rot_tweener.ChangeEndValue(v_rot, true).Restart();
    }
}
