using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatMessageBlock : MonoBehaviour
{
    private TMP_Text chatMessage;

    private void Awake()
    {
        chatMessage = GetComponentInChildren<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMessage(string message)
    { 
        // TODO 송신 타입에 따라서 색 바뀌게
        chatMessage.text = message;
    }
}
