using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clickgamestart()
    {
        SceneManager.LoadScene("AnimalGame");
    }
    

    public void ClickOXQUIZ()
    {
        SceneManager.LoadScene("leftright");
    }

    public void ClickHexaGone()
    {
        SceneManager.LoadScene("FALLGAME");
    }

    public void ClickGame4()
    {
        SceneManager.LoadScene("game4");
    }
}
