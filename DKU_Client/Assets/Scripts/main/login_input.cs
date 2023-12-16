using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class login_input : MonoBehaviour
{
    public static login_input instance = null;
    
    public GameObject sever;
    public GameObject logincanvas;
    public GameObject signincanvas;
    public GameObject loadingcanvas;
    public GameObject Loginfailcanvas;
    public GameObject registfailcanvas;
    
    public TMP_InputField Input_ID;
    public TMP_InputField Input_PWD;
    public TMP_InputField Signin_ID;
    public TMP_InputField Signin_PWD;
    public TMP_InputField Signin_NAME;
    public TMP_Text errorMSG;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void erroroff()
    {
        if (Loginfailcanvas.activeSelf)
        {
            Loginfailcanvas.SetActive(false);
        }if (registfailcanvas.activeSelf)
        {
            registfailcanvas.SetActive(false);
        }
    }
    public void loginfail()
    {
        Loginfailcanvas.SetActive(true);
        Invoke("erroroff",1f);
    }public void registfail()
    {
        registfailcanvas.SetActive(true);
        Invoke("erroroff",1f);
    }

    public void ClickLogin()
    {
        //Debug.Log("Login");
        string id = Input_ID.text;
        string pw = Input_PWD.text;
        
        MemberService.Login_Request(id,pw);
        loadingcanvas.SetActive(true);
    }

    public void Make_ID()
    {
        logincanvas.SetActive(false);
        signincanvas.SetActive(true);
    }

    public void back()
    {
        logincanvas.SetActive(true);
        signincanvas.SetActive(false);
    }
    public void ClickSignin()
    {
        if (Signin_ID.text.IsNullOrWhitespace())
        {
            // errorMSG.text = "아이디를 입력하세요";
            Signin_ID.placeholder.GetComponent<TMP_Text>().text = "아이디를 입력하세요";
            Signin_ID.placeholder.GetComponent<TMP_Text>().color = Color.red;
            return;
        }if (Signin_PWD.text.IsNullOrWhitespace())
        {
            Signin_PWD.placeholder.GetComponent<TMP_Text>().text = "비밀번호를 입력하세요";
            Signin_PWD.placeholder.GetComponent<TMP_Text>().color = Color.red;
            return;
        }if (Signin_NAME.text.IsNullOrWhitespace())
        {
            Signin_NAME.placeholder.GetComponent<TMP_Text>().text = "닉네임를 입력하세요";
            Signin_NAME.placeholder.GetComponent<TMP_Text>().color = Color.red;
            return;
        }
        Debug.Log(Signin_NAME.text);
        MemberService.Register_Request(Signin_ID.text,Signin_PWD.text,Signin_NAME.text);
        
        
    }
}
