using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quizEnter : MonoBehaviour
{
    public GameObject EventObj;
    public GameObject QuizeObj;
    public GameObject ScoreObj;
    public GameObject AnswerObj;
    public GameObject Button;
    public GameObject QuizNubObj;
    public void GameStart()
    {
        EventObj.GetComponent<oxtext>().init();
        QuizeObj.SetActive(true);
        AnswerObj.SetActive(true);
        EventObj.SetActive(true);
        QuizNubObj.SetActive(true);
        Button.SetActive(false);
    }

    public void Update()
    {
        if (EventObj.GetComponent<oxtext>().quizNumber == 6)
        {
            QuizeObj.SetActive(false);
            AnswerObj.SetActive(false);
            EventObj.SetActive(false);
            QuizNubObj.SetActive(false);
            Button.SetActive(true);
        }
    }
}
