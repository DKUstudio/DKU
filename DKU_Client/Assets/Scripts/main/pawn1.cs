using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pawn1 : MonoBehaviour
{
    public GameObject gameButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        gameButton.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            gameButton.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        gameButton.SetActive(false);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            gameButton.SetActive(false);
        }
    }
}
