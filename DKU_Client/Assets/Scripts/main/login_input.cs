using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class login_input : MonoBehaviour
{
    public GameObject sever;
    public GameObject logincanvas;
    public GameObject signincanvas;
    
    public TMP_InputField Input_ID;
    public TMP_InputField Input_PWD;
    public TMP_InputField Signin_ID;
    public TMP_InputField Signin_PWD;
    public TMP_InputField Signin_NAME;

    public void ClickLogin()
    {
        Debug.Log("Login");
        string id = Input_ID.text;
        string pw = Input_PWD.text;
        MemberService.Login_Request(id,pw);
    }

    public void Make_ID()
    {
        logincanvas.SetActive(false);
        signincanvas.SetActive(true);
    }

    public void ClickSignin()
    {
        MemberService.Register_Request(Signin_ID.text,Signin_PWD.text,Signin_NAME.text);
        logincanvas.SetActive(true);
        signincanvas.SetActive(false);
    }
}
