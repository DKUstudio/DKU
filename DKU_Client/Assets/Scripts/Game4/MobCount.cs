using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCount : MonoBehaviour
{
    //몹 총개수, 현재 개수에 대한 정보 저장
    public int totalMob;
    public int currentMob;
    
    // Start is called before the first frame update
    void Start()
    {
        totalMob = gameObject.GetComponent<mobGenerator>().Mobtotal();
        currentMob = totalMob;
    }

    public int TotalMob()
    {
        return totalMob;
    }

    public int CurrentMob()
    {
        return currentMob;
    }
    public void Currentmobminus()
    {
        currentMob -= 1;
        gameObject.GetComponent<GAME4Canvas>().CurrentMobMinus();
    }
}
