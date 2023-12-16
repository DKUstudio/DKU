using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modelSelector : MonoBehaviour
{
    public Transform model;
    public int modelCount;
    public int modelnum;
    private void Start()
    {
        modelCount = model.childCount - 1;
        modelnum = 0;
    }

    public void ChangeModel(int moveto)
    {
        if (moveto == 0)
        {
            model.GetChild(modelnum).gameObject.SetActive(false);
            modelnum = modelnum + 1 < modelCount ? modelnum + 1 : 0;
            model.GetChild(modelnum).gameObject.SetActive(true);
        }
        else
        {
            model.GetChild(modelnum).gameObject.SetActive(false);
            modelnum = modelnum > 0 ? modelnum - 1 : modelCount - 1;
            model.GetChild(modelnum).gameObject.SetActive(true);
        }
        Debug.Log("CHANGE MODEL to " + model.GetChild(modelnum).name);
    }

    public void ChangeToPlayer()
    {
        PlayerInfo.instance.ChangeShift(modelnum);
        // modelnum: 시프트
        MemberService.CharaDataShiftChanged((short)modelnum); // shift 정보 갱신
    }
}
