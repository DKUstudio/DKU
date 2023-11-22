using System.Collections;
using System.Collections.Generic;
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
}
