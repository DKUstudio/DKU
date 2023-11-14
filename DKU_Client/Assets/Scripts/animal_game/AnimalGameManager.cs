using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalGameManager : MonoBehaviour
{
    public static AnimalGameManager instance;
    public TMP_Text scoreText;
    public int score = 0;
    public TMP_Text Lastscore;
    private bool gameover = false;
    public GameObject endui;
    public GameObject menuui;
    public Transform zoo;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("게임매니저 중복!");
            Destroy(gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void menuon()
    {
        if (menuui.activeSelf)
        {
            menuui.SetActive(false);
        }
        else
        {
            menuui.SetActive(true);
        }
    }
    
    [Button]
    public void addPoint(int p)
    {
        score += p;
        scoreText.text = score.ToString();
    }

    public void endGame()
    {
        if (gameover)
        {
            return;
        }

        gameover = true;
        //score 
        Debug.Log("ENDGAME---");
        Lastscore.text = score.ToString();
        endui.SetActive(true);
    }

    public void initgame()
    {
        score = 0;
        scoreText.text = score.ToString();
        endui.SetActive(false);
        gameover = false;
        menuui.SetActive(false);
        animal[] zoos = zoo.gameObject.GetComponentsInChildren<animal>();
        if (zoos == null)
        {
            Debug.Log("no animals");
            return;
        }
        foreach (animal a in zoos)
        {
            Debug.Log(a.name);
            Destroy(a.gameObject);
        }
    }

    public void exitgame()
    {
        initgame();
        
        SceneManager.LoadScene("MainMap");
    }
}
