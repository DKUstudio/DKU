using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class spawnController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject[] animals;
    
    public Transform spawnY;
    public Transform spawner;
    public int randomint = 0;
    // Start is called before the first frame update
    void Start()
    {
        randomint = Random.Range(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnBeginDrag(PointerEventData beginPoint)
    {
        spawner.transform.position = new Vector3(beginPoint.position.x,spawnY.position.y,spawner.transform.position.z);
        
    }

    public void OnDrag(PointerEventData draggingPoint)
    {
        spawner.transform.position = new Vector3(draggingPoint.position.x,spawnY.position.y,spawner.transform.position.z);

    }

    public void OnEndDrag(PointerEventData endPoint)
    {
        
    }
    
    void inputNewAnimal()
    {
        
    }
}
