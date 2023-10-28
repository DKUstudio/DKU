using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menubar;
    
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

    public void ExitClick()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit(); // 어플리케이션 종료
    #endif


    }

}
