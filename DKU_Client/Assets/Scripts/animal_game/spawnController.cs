using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;


public class spawnController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public static spawnController instance = null;
    public GameObject[] animals;
    public GameObject[] nextanimal;
    public Transform zoo;
    public float cooltime = 0.3f;
    public bool cool = false;
    public Transform spawnY;
    public Transform spawner;
    public int randomint = 0;

    private GameObject nowAnimal;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    void Start()
    {
        randomint = Random.Range(0, 4);
        MakeAnimal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeAnimal()
    {
        nowAnimal = Instantiate(animals[randomint], new Vector3(spawner.position.x,spawnY.position.y, 0),
            animals[randomint].transform.rotation, spawner);
        nowAnimal.GetComponent<Rigidbody2D>().simulated = false;
        randomint = Random.Range(0, 4);
        cool = false;
        for (int i = 0; i < nextanimal.Length; i++)
        {
            if (i==randomint)
            {
                nextanimal[i].SetActive(true);
            }
            else
            {
                nextanimal[i].SetActive(false);
            }
        }
    }
    public void OnPointerClick(PointerEventData ClickedPoint)
    {
        if (cool) return;
        Debug.Log(ClickedPoint.position);
        spawner.transform.position = new Vector3(ClickedPoint.position.x * (26f/Screen.width),spawnY.position.y,spawner.transform.position.z);
        // Instantiate(animals[randomint], new Vector3(ClickedPoint.position.x * (26f/Screen.width),spawnY.position.y, 0),
        //     animals[randomint].transform.rotation, transform);
        nowAnimal.GetComponent<Rigidbody2D>().simulated = true;
        nowAnimal.transform.parent = zoo;
        cool = true;
        Invoke("MakeAnimal",cooltime);
    }
    public void OnBeginDrag(PointerEventData beginPoint)
    {        
        Debug.Log(beginPoint.position);

        spawner.transform.position = new Vector3(beginPoint.position.x * (26f/Screen.width),spawnY.position.y,spawner.transform.position.z);
        
    }

    public void OnDrag(PointerEventData draggingPoint)
    {
        Debug.Log(draggingPoint.position);
        spawner.transform.position = new Vector3(draggingPoint.position.x * (26f/Screen.width),spawnY.position.y,spawner.transform.position.z);

    }

    public void OnEndDrag(PointerEventData endPoint)
    {
        // Instantiate(animals[randomint], new Vector3(endPoint.position.x * (26f/Screen.width), spawnY.position.y, 0),
        //     animals[randomint].transform.rotation);
        // randomint = Random.Range(0, 10);
    }
    
    public void LevelUp(GameObject a, GameObject b)
    {
        
        
        float newX = (a.transform.position.x + b.transform.position.x) / 2;
        float newY = (a.transform.position.y + b.transform.position.y) / 2;
        Vector3 newpos = new Vector3(newX, newY, 0);
        int newLv = a.GetComponent<animal>().Level+1;
        Destroy(a);
        Destroy(b);
        Instantiate(animals[newLv], new Vector3(newX,newY, 0),
            animals[newLv].transform.rotation, zoo);
        
    }
}
