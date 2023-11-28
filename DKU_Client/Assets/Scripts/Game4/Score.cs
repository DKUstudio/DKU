using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    //플레이어 레벨 관련 
    public int Score = 1;
    public TMP_Text ScoreText;

    public void Start()
    {
        ScoreText.text = "";
    }

    public int PlayerLevel()
    {
        return Score;
    }

    public void PlayerLevelUp()
    {
        Score += 1;
        ScoreText.text = ""+Score;
    }
    
    
    
}
