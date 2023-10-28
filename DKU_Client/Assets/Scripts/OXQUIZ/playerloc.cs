using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerloc : MonoBehaviour
{
    public bool playerLoc;

    private void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            playerLoc = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            playerLoc = false;
        }
    }
}
