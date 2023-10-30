using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI : MonoBehaviour
{
    // 모든 UI는 켜고 끌 수 있고, 
    public abstract void Turn_On();
    public abstract void Turn_Off();

}
