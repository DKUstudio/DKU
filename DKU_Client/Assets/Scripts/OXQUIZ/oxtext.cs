using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class oxtext : MonoBehaviour
{
    public TMP_Text countdownText;
    public TMP_Text question;

    public float limitTime;

    private List<List<string>> _questionList = new List<List<string>>();
    
    //정답인지 
    public int answer;
    //public 받은 시간 저장해두기
    public float timeData;
    
    // Start is called before the first frame update
    void Start()
    {
        timeData = limitTime;
        _questionList.Add(new List<string> { "단국대에는 잘생긴 사람만 들어올 수 있다.", "1"});
        _questionList.Add(new List<string> { "단국대에는 못생긴 사람만 들어올 수 있다.", "0"});
        _questionList.Add(new List<string> { "안녕하세요는 Hi라는 뜻이다.", "1"});
        _questionList.Add(new List<string> { "안녕히가세요는 bye라는 뜻이다.", "1"});
        _questionList.Add(new List<string> { "이현우는 키가 185이다..", "0"});
        _questionList.Add(new List<string> { "박새결은 안경을 쓰고 있다.", "0"});
        _questionList.Add(new List<string> { "박정호는 롤을 사랑한다.", "0"});
        ShowQuestion();
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
            StartCoroutine( WaitQustion());
        }
        
    }

   

    void ShowQuestion()
    {
        int questionNumber = Random.Range(0, _questionList.Count);
        question.text = _questionList[questionNumber][0];
        answer = int.Parse(_questionList[questionNumber][1]);
    }

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
    
    void CountDown()
    {
        limitTime -= Time.deltaTime;
        countdownText.text = " " + Mathf.Round(limitTime) +" " ;
    }

    IEnumerator WaitQustion()
    {
        yield return new WaitForSeconds(2.0f);
        limitTime = timeData;
        ShowQuestion();
    }
}
