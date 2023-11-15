using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MobCollsion : MonoBehaviour
{
    //몹에 대한 정보 
    public float size;
    public int level;
    public GameObject MobEvent;

    public TMP_Text levelText;
    
    // Start is called before the first frame update
    void Start()
    {
        level = Random.Range(1,10);
        levelText.text = "level " + level;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("player"))
        {
            if (level<= MobEvent.GetComponent<Level>().PlayerLevel())
            {
                //몹 카운트 -1;
                MobEvent.GetComponent<MobCount>().Currentmobminus();
                MobEvent.GetComponent<Level>().PlayerLevelUp();
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Can't destroy");
            }
        }
    }
}
