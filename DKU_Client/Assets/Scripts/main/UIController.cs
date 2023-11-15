using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DKU_ServerCore;
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clickgamestart()
    {
        WorldService.ChangeWorld(CommonDefine.WorldBlockType.Suika_Game);

        //SceneManager.LoadScene("AnimalGame");
    }
    

    public void ClickOXQUIZ()
    {
        // �̴ϰ��� ������ ������ �������� Ȯ���ϴ� ��Ŷ �۽� (������ ȸ�Ź��� Handler���� ����)

        WorldService.ChangeWorld(CommonDefine.WorldBlockType.OX_quiz);

        //SceneManager.LoadScene("leftright");
    }

    public void ClickHexaGone()
    {
        WorldService.ChangeWorld(CommonDefine.WorldBlockType.Hexagon);

        //SceneManager.LoadScene("FALLGAME");
    }

    public void ClickGame4()
    {
        SceneManager.LoadScene("game4");
    }
}
