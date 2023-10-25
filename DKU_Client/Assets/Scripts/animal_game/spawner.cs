using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class spawner : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject[] animals;
    
    public Transform spawnY;
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
        
    }

    public void OnDrag(PointerEventData draggingPoint)
    {
        
    }

    public void OnEndDrag(PointerEventData endPoint)
    {
        
    }
    
    void inputNewAnimal()
    {
        
    }
}
