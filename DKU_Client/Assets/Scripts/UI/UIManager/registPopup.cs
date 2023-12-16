using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using TMPro;

public class registPopup : UIpopup
{
    public TMP_InputField Signin_ID;
    public TMP_InputField Signin_PWD;
    public TMP_InputField Signin_NAME;
        
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
        
        UIManager.instance.Close(this);
        
        
    }
}
