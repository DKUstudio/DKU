using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Click_Event : MonoBehaviour
{
    public GameObject top;

    public GameObject start_button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click_Start()
    {
        top.SetActive(false);
        start_button.SetActive(false);
    }
}
