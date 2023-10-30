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
    private List<List<string>> _questionList = new List<List<string>>();
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
        _questionList.Add(new List<string> { "단국대에는 잘생긴 사람만 들어올 수 있다.", "1"});
        _questionList.Add(new List<string> { "단국대에는 못생긴 사람만 들어올 수 있다.", "0"});
        _questionList.Add(new List<string> { "안녕하세요는 Hi라는 뜻이다.", "1"});
        _questionList.Add(new List<string> { "안녕히가세요는 bye라는 뜻이다.", "1"});
        _questionList.Add(new List<string> { "이현우는 키가 185이다..", "1"});
        _questionList.Add(new List<string> { "박새결은 안경을 쓰고 있다.", "1"});
        _questionList.Add(new List<string> { "박정호는 롤을 사랑한다.", "0"});
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
        int questionNumber = Random.Range(0, _questionList.Count);
        question.text = _questionList[questionNumber][0];
        answer = int.Parse(_questionList[questionNumber][1]);
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
