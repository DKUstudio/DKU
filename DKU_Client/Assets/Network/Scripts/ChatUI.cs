using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatUI : MonoBehaviour
{
    private static ChatUI instance;
    public static ChatUI Instance => instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        //this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        panel_chatDisplay.SetActive(false);
        panel_chatInputField.SetActive(false);
    }

    #region chatDisplay
    [SerializeField][Required] private GameObject button_chatDisplayOnOff;
    [SerializeField][Required] private TMP_Text text_chatDisplayOnOff;
    [SerializeField][Required] private GameObject panel_chatDisplay;
    [SerializeField][Required] private GameObject chatBlockFactory;
    [SerializeField][Required] private Transform content_chatBlock;
    [SerializeField][Required] private RectTransform rect_content_panel;
    private int blockIdx = 0;
    public void Button_ChatDIsplay_On()
    {
        button_chatDisplayOnOff.SetActive(false);
        panel_chatDisplay.SetActive(true);
        panel_chatInputField.SetActive(true);
    }
    public void Button_ChatDisplay_Off()
    {
        button_chatDisplayOnOff.SetActive(true);
        panel_chatDisplay.SetActive(false);
        panel_chatInputField.SetActive(false);
    }
    StringBuilder sb = new StringBuilder();
    const string global_chat_korean = "일반";
    const string local_chat_korean = "지역";
    public void RecvMessage(ChatData chatData)
    {
        Debug.Log("text recved");

        try
        {
            GameObject block = Instantiate(chatBlockFactory);
            block.transform.SetParent(content_chatBlock);

            RectTransform rect = block.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(25, 0);
            rect.offsetMax = new Vector2(-25, 100);
            rect.anchoredPosition = new Vector3(0, blockIdx++ * -100, 0);
            rect_content_panel.offsetMin = new Vector2(0, 0);
            rect_content_panel.offsetMax = new Vector2(0, blockIdx * 100);

            ChatMessageBlock chat = block.GetComponent<ChatMessageBlock>();

            string color_code = "";
            string recv_type = "";
            switch ((ChatRecvType)chatData.recv_target_group)
            {
                case ChatRecvType.To_All:
                    color_code = "<color=#000000>";
                    recv_type = global_chat_korean;
                    break;
                case ChatRecvType.To_Local:
                    color_code = "<color=#ee4328>";
                    recv_type = local_chat_korean;
                    break;
                default:
                    break;
            }

            sb.Clear();
            sb.Append(color_code);
            sb.Append("[");
            sb.Append(recv_type);
            sb.Append("] ");
            sb.Append(chatData.sender_data.nickname);
            sb.Append(": ");
            sb.Append(chatData.message);
            sb.Append("</color>");

            string display_msg = sb.ToString();
            text_chatDisplayOnOff.text = display_msg;
            chat.SetMessage(display_msg);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    #endregion

    #region chatInput
    [SerializeField][Required] private GameObject panel_chatInputField;
    [SerializeField][Required] private TMP_Dropdown dropdown_MessageType;
    [SerializeField][Required] private TMP_InputField inputfield_chatSpace;
    [SerializeField][Required] private Button button_chatSubmit;
    public void Button_ChatSubmit()
    {
        if (NetworkManager.Instance.UDATA == null)
            return;
        if (inputfield_chatSpace.text.IsNullOrWhitespace())
            return;
        string msg = inputfield_chatSpace.text;
        inputfield_chatSpace.text = "";

        ChatData cdata = new ChatData();
        cdata.sender_data = NetworkManager.Instance.UDATA;
        cdata.recv_target_group = (short)dropdown_MessageType.value;
        cdata.message = msg;
        ChatService.Chat(cdata);
        // C_ChatReq req = new C_ChatReq();
        // req.chatData.sender_data = NetworkManager.Instance.UDATA;
        // req.chatData.recv_target_group = (short)dropdown_MessageType.value;
        // req.chatData.message = msg;
        // byte[] body = req.Serialize();

        // Packet pkt = new Packet(PacketType.C_ChatReq, body, body.Length);
        // NetworkManager.Instance.Connections.Send(pkt);
    }
    #endregion
}
