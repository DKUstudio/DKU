using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LEADER_UI : MonoBehaviour
{
    public List<List<string>> LeaderBoardData = new List<List<string>>();
    public GameObject content;
    public TMP_Text playername;
    public TMP_Text rank;
    public TMP_Text score;
    public GameObject a;
    
    // Start is called before the first frame update
    void Start()
    {
        LeaderBoardData.Add(new List<string>{"박새결","10"});
        LeaderBoardData.Add(new List<string>{"태민규","9"});
        LeaderBoardData.Add(new List<string>{"박정호","8"});
        LeaderBoardData.Add(new List<string>{"김민준","7"});
        LeaderBoardData.Add(new List<string>{"이현우","7"});
        LeaderBoardData.Add(new List<string>{"이혜성","7"});
        
        content_add();
    }

    void content_add()
    {
        Debug.Log("Leaderboard count= "+LeaderBoardData.Count);
        for (int i = 0; i < LeaderBoardData.Count; i++)
        {
            a= content.transform.GetChild(i).gameObject;
            a.SetActive(true);
            //playername
             playername  = a.transform.GetChild(1).gameObject.GetComponent<TMP_Text>(); 
             rank  = a.transform.GetChild(2).gameObject.GetComponent<TMP_Text>(); 
             score = a.transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
             rank.text = (i+1).ToString(); 
             playername.text  = LeaderBoardData[i][0];
             score.text = LeaderBoardData[i][1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
