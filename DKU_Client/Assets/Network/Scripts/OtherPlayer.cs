using System.Collections;
using System.Collections.Generic;
using DKU_ServerCore.Packets;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;

public class OtherPlayer : MonoBehaviour
{
    [ShowInInspector][ReadOnly] private UserData udata;
    public UserData UDATA => udata;

    private Tweener pos_tweener;
    private Tweener rot_tweener;

    Animator anim;
    [SerializeField] TMP_Text nickname;
    int charaCount = 18;

    private void Update()
    {
        nickname.transform.rotation = Camera.main.transform.rotation;
    }


    public void SetUserData(UserData v_udata)
    {
        udata = v_udata;
        nickname.text = v_udata.nickname;
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

    public void CharaChangeTo(short v_shift)
    {
        for (short i = 0; i < charaCount; i++)
        {
            if (i == v_shift)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        anim = transform.GetChild(v_shift).GetComponent<Animator>();
    }

    public void AnimationChangeTo(string v_animName)
    {
        anim?.Play(v_animName);
    }
}
