using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MobCollsion : MonoBehaviour
{
    public float size;
    
    // Start is called before the first frame update
    void Start()
    {
        size = Random.Range(0.5f, 3f);
        gameObject.transform.localScale = new Vector3(size,size,size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("player"))
        {
            if (collision.transform.localScale.x > transform.localScale.x)
            {
                float x = collision.transform.localScale.x;
                float y = collision.transform.localScale.y;
                float z = collision.transform.localScale.z;
                collision.transform.localScale = new Vector3(x * 1.05f, y * 1.05f, z * 1.05f);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Can't destroy");
            }
        }
    }
}
