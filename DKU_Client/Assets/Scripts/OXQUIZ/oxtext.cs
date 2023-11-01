using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class oxtext : MonoBehaviour
{
    public TMP_Text countdownText;
    public TMP_Text question;
    public TMP_Text scoreText;

    //제한시간
    public float limitTime;
    //문제,답
    public List<List<string>> questionList=new List<List<string>>();
    //정답인지 
    public int answer;
    //public 받은 시간 저장해두기
    public float timeData;
    
    //score저장용
    public GameObject Oloc;
    public GameObject Xloc;
    //점수카운트
    public int scorecnt;
    
    //문제 카운트
    public int quizNumber;
    public TMP_Text quizNumberText;
    
    void Start()
    {
        init();
        timeData = limitTime;
        questionList.Add(new List<string> { "나무는 tree이다.", "1"});
        questionList.Add(new List<string> { "공부는 즐겁다", "1"});
        questionList.Add(new List<string> { "stl은 몰라도 된다.", "0"});
        questionList.Add(new List<string> { "알고리즘은 재미없다", "0"});
        questionList.Add(new List<string> { "CS 준비 안해도 면접에서 문제 없다.", "0"});
        questionList.Add(new List<string> { "면접 보기 전에 회사에 대한 정보를 숙지 해야 한다.", "1"});
        ShowQuestion();
        showQuizNumber();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (limitTime > 0)
        {
            CountDown();
        }
        else if (limitTime < 0)
        {
            limitTime = 0;
            ShowAnswer();
            score();
            StartCoroutine( WaitQustion());
        }
        
    }
    
    //init
    public void init()
    {
        scorecnt = 0;
        quizNumber = 1;
        
        scoreText.text = "" + scorecnt;
        quizNumberText.text = "문제." + quizNumber;
    }
    
    //문제 번호 출력
    void showQuizNumber()
    {
        quizNumber += 1;
        quizNumberText.text = "문제." + quizNumber;
    } 
    //문제 랜덤 출력
    void ShowQuestion()
    {
        int questionNumber = Random.Range(0, questionList.Count);
        question.text = questionList[questionNumber][0];
        answer = int.Parse(questionList[questionNumber][1]);
    }
    //정답 출력
    void ShowAnswer()
    {
        if (answer == 1)
        {
            countdownText.text = "정답은 O!";
        }
        else
        {
            countdownText.text = "정답은 X!";
        }
    }
    //카운트다운
    void CountDown()
    {
        limitTime -= Time.deltaTime;
        countdownText.text = " " + Mathf.Round(limitTime) +" " ;
    }
    //정답 보여주고 몇초 후 다음 문제 출력
    IEnumerator WaitQustion()
    {
        yield return new WaitForSeconds(4.0f);
        limitTime = timeData;
        showQuizNumber();
        ShowQuestion();
    }
    
    
    //정답 개수 저장
    void score()
    {
        
        if (Oloc.GetComponent<playerloc>().playerLoc==true)
        {
            if (answer == 1)
            {
                scorecnt += 1;
            }
        }
        if(Xloc.GetComponent<playerloc>().playerLoc==true)
        {
            if (answer == 0)
            {
                scorecnt += 1;
            }
        }

        scoreText.text = "" + scorecnt;
    }
    
}
