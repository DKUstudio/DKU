using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GAME4Canvas : MonoBehaviour
{
    //몹 카운트, 시간
    public TMP_Text totalMob;
    public TMP_Text currentMob;
    
    void Start()
    {
        totalMob.text = gameObject.GetComponent<MobCount>().TotalMob().ToString();
        currentMob.text = gameObject.GetComponent<MobCount>().CurrentMob().ToString();
        
    }
 
    public void CurrentMobMinus()
    {
        currentMob.text = gameObject.GetComponent<MobCount>().CurrentMob().ToString();
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
