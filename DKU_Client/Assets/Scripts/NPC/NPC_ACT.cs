using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ACT : MonoBehaviour
{
    public GameObject canmera;

    private GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        text.transform.rotation = canmera.transform.rotation;
    }
}
