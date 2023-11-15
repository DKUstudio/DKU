using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobGenerator : MonoBehaviour
{
    public GameObject monster;
    
    public List<GameObject> spawner;

    public int total = 1;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawner.Count; i++)
        {
            Vector3 loc = spawner[i].transform.position;
            int cnt = Random.Range(2, 5);
            total += cnt;
            for (int j = 0; j < cnt; j++)
            {
                GameObject gen= Instantiate(monster);
                loc.x += Random.Range(2, 6);
                loc.z += Random.Range(2, 6);
                gen.transform.position = loc;
            }
        }

        gameObject.GetComponent<MobCount>().enabled = true;
        gameObject.GetComponent<GAME4Canvas>().enabled = true;

    }

    public int Mobtotal()
    {
        return total;
    }
}
