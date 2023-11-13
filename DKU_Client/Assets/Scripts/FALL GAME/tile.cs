using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{
    private float fadetime= 3.0f;
    public Material mat;

    public float timer;

    public bool ONOFF=false;

    public float alpha = 1;
    // Start is called before the first frame update
    void Start()
    {
        mat = gameObject.GetComponent<Renderer>().material;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ONOFF)
        {
            timer = Time.deltaTime;
            alpha -= (timer / fadetime);
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, alpha);
            if (alpha <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("player"))
        {
            ONOFF = true;
        }
    }
    
    
    
}
