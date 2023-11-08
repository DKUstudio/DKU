using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menubar;
    public GameObject leaderboard;
    public GameObject inven;
    
    public void MenuClick()
    {
        menubar.SetActive(true);
    }

    public void LobbyClick()
    {
        SceneManager.LoadScene("Scenes/MainMap");
    }

    public void CancelClick()
    {
        menubar.SetActive(false);
    }

    public void inven_Click()
    {
        inven.SetActive(true);
    }

    public void inven_Cancel()
    {
        inven.SetActive(false);
    }


public void leaderboard_Click()
    {
        leaderboard.SetActive(true);
        gameObject.GetComponent<LEADER_UI>().enabled = true;
    }

    public void leaderboard_Cancel()
    {
        leaderboard.SetActive(false);
        gameObject.GetComponent<LEADER_UI>().enabled = false;
    }
    public void ExitClick()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit(); // 어플리케이션 종료
    #endif


    }

}
