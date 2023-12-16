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
        // TODO �۽� Ÿ�Կ� ���� �� �ٲ��
        chatMessage.text = message;
    }
}
