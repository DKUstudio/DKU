using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game4Manager : MonoBehaviour
{
    public GameObject startButton;
    public GameObject scoreBoard;
    public TMP_Text ScoreVal;
    private mobGenerator mobGen;
    private gameTimer gametimer;
    private MobCount _mobCount;
    
    public void Start()
    {
        mobGen = gameObject.GetComponent<mobGenerator>();
        gametimer = gameObject.GetComponent<gameTimer>();
        _mobCount = gameObject.GetComponent<MobCount>();
    }

    public void press_start()
    {
        mobGen.enabled = true;
        gametimer.enabled = true;
        startButton.SetActive(false);
    }

    public void EndGame()
    {
        ScoreBoard();
        gametimer.enabled = false;
        gameObject.GetComponent<MobCount>().enabled = false;
        mobGen.enabled = false;
    }

    public void ScoreBoard()
    {
        scoreBoard.SetActive(true);
        int scoreval = gameObject.GetComponent<Level>().PlayerLevel();
        ScoreVal.text = "" + scoreval;
    }
    public void SceneLobby()
    {
        SceneManager.LoadScene("MainMap");
    }
    
}
