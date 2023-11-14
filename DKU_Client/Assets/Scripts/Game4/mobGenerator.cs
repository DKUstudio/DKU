using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobGenerator : MonoBehaviour
{
    public GameObject monster;

    public List<GameObject> spawner;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawner.Count; i++)
        {
            Vector3 loc = spawner[i].transform.position;
            int cnt = Random.Range(2, 5);
            for (int j = 0; j < cnt; j++)
            {
                GameObject gen= Instantiate(monster);
                gen.transform.position = new Vector3(gen.transform.position.x+Random.Range(2,6),gen.transform.position.y,gen.transform.position.z+Random.Range(2,6));
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}