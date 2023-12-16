using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailTile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("player"))
        {
            collision.transform.position = new Vector3(20f, 20f, 20f);
            
        }
    }
}
