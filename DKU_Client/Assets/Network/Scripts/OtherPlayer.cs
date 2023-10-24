using System.Collections;
using System.Collections.Generic;
using DKU_ServerCore.Packets;
using UnityEngine;

public class OtherPlayer : MonoBehaviour
{
    private UserData udata;
    public UserData UDATA => udata;
    public void SetUserData(UserData v_udata)
    {
        udata = v_udata;
    }
}
