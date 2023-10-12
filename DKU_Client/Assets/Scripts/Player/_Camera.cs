using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Camera : MonoBehaviour
{
    public GameObject player;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = this.transform.position;
        this.gameObject.transform.position = Vector3.Lerp(pos, player.transform.position, 0.4f);
    }
}
