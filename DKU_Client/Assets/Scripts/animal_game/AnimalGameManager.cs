using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class AnimalGameManager : MonoBehaviour
{
    public static AnimalGameManager instance;
    public TMP_Text scoreText;
    public int score = 0;
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
    [Button]
    public void addPoint(int p)
    {
        score += p;
        scoreText.text = score.ToString();
    }
}
