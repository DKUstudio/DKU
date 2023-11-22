using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;
    public PlayerController player;
    
    public int bitmask;
    public short bitshift;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
    }

    public void ServerData(int bit, short sh)
    {
        bitmask = bit;
        bitshift = sh;
    }

    public void ChangeShift(int modelNum)
    {
        bitshift = (short)modelNum;
        player.ChangeModel(modelNum);
    }
}
