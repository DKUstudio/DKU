using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour, IPointerClickHandler
{
    public static UIManager instance;

    [SerializeField] private Stack<UIpopup> uistack;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            uistack = new Stack<UIpopup>();
        }
        else
        {
            Debug.LogWarning("UI매니저 중복!");
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                ClosePeek();
                Debug.Log("CLOSE CLICK");
            }
            
            
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("CLICKED!");
        // Debug.Log(EventSystem.current.gameObject.name);
    }
    
    //UIpopup을 인자로 받아와서 활성화시켜준다
    public void Open(UIpopup popup)
    {
        if (popup != null)
        {
            UIUtilities.UIActive(popup.gameObject,true);
            uistack.Push(popup);
        }
    }

    public void Close(UIpopup popup)
    {
        if (popup != null && uistack.Contains(popup))
        {
            UIUtilities.UIActive(popup.gameObject,false);
            Debug.Log(uistack.Peek().gameObject.name);
        }
    }
    // 가장 최근에 활성화된 UI를 스택에서 꺼내 비활성화시킨다
    [Button]
    public void ClosePeek()
    {
        while (uistack.Count > 0)
        {
            if (uistack.Peek().gameObject.activeSelf)
            {   
                UIUtilities.UIActive(uistack.Pop().gameObject,false);
                break;
            }
            else
            {
                uistack.Pop();
            }
        }
    }
}
