using System.Collections;
using System.Collections.Generic;
using DKU_ServerCore;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIpopup : MonoBehaviour,IPointerClickHandler
{
    public GameObject popup;
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        // Debug.Log("CLICKED!");
        // Debug.Log(EventSystem.current.gameObject.name);
    }

    public void GO_Out()
    {
        Application.Quit();
    }public void GO_mainmap()
    {
        WorldService.ChangeWorld(CommonDefine.WorldBlockType.Dankook_University);
    }
}
