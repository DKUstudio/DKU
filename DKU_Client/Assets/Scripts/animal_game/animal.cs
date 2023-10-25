using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animal : MonoBehaviour
{
    public int Level;

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.TryGetComponent<animal>(out animal val);
        if (val != null)
        {
            if (val.Level == Level)
            {
                if (transform.position.x + transform.position.y > val.transform.position.x + val.transform.position.y)
                {
                    Debug.Log("Hit");
                    spawnController.instance.LevelUp(this.gameObject, col.gameObject);
                }
                // Instantiate(GetComponentInParent<spawnController>().animals[Level + 1], newpos, transform.rotation);
                // Destroy(col.gameObject);
                // Destroy(this.gameObject);
            }
        }
    }
}