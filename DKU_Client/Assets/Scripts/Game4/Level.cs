using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    //플레이어 레벨 관련 
    public int playerLevel = 1;
    public TMP_Text playerLevelText;

    public void Start()
    {
        playerLevelText.text = "Level 1";
    }

    public int PlayerLevel()
    {
        return playerLevel;
    }

    public void PlayerLevelUp()
    {
        playerLevel += 1;
        playerLevelText.text = "Level "+playerLevel;
    }
    
    
    
}
