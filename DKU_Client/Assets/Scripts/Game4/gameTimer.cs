using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameTimer : MonoBehaviour
{
    public TMP_Text countdownText;
    public float limitTime;
    
    public bool timeEnd=false;
    // Start is called before the first frame update
    void Start()
    {
        limitTime = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (limitTime > 0)
        {
            CountDown();
        }
        else
        {
            gameObject.GetComponent<Game4Manager>().EndGame();
        }
    }

    void CountDown()
    {
        limitTime -= Time.deltaTime;
        countdownText.text = " " + Mathf.Round(limitTime) +" " ;
    }
    
}

