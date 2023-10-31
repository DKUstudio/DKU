using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class quizEnter : MonoBehaviour
{
    public GameObject EventObj;
    public GameObject QuizeObj;
    public GameObject ScoreObj;
    public GameObject AnswerObj;
    public GameObject Button;
    public GameObject QuizNubObj;
    public GameObject user;

    public int userLoc;
    public int gameEnd;
    public void GameStart()
    {
        EventObj.GetComponent<oxtext>().init();
        QuizeObj.SetActive(true);
        AnswerObj.SetActive(true);
        EventObj.SetActive(true);
        QuizNubObj.SetActive(true);
        Button.SetActive(false);
        user.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), 0.5f, Random.Range(-1.0f, 1.0f));
        userLoc = 1;
        gameEnd = 0;
    }

    public void Update()
    {
        if (EventObj.GetComponent<oxtext>().quizNumber == 6 && gameEnd==0)
        {
            gameEnd = 1;
            QuizeObj.SetActive(false);
            AnswerObj.SetActive(false);
            EventObj.SetActive(false);
            QuizNubObj.SetActive(false);
            Button.SetActive(true);
            if (userLoc == 1)
            {
                user.transform.position = new Vector3(Random.Range(-35.0f, -36.0f), 7.2f, Random.Range(-23.6f, -23.0f));
                userLoc = 0;
            }
        }
    }
}
