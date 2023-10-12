using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamRotate : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public GameObject player;
    public Transform camPivot;
    public float rotationSpeed = 0.4f;
    public float max_xAngle;
    public float min_xAngle;

    Vector3 beginPos;
    Vector3 draggingPos;

    Vector2 prevPoint;
    float cameraPitch = 35f;
    
    float xAngle;
    float yAngle;
    float xAngleTmp;
    float yAngleTmp;
    
    void Start()
    {
        xAngle = camPivot.rotation.eulerAngles.x;
        yAngle = camPivot.rotation.eulerAngles.y;
    }


    public void OnBeginDrag(PointerEventData beginPoint)
    {
        beginPos = beginPoint.position;
        xAngleTmp = xAngle;
        yAngleTmp = yAngle;
    }

    public void OnDrag(PointerEventData draggingPoint)
    {
        draggingPos = draggingPoint.position;
        
        // this.transform.RotateAround(player.transform.position,Vector3.up, -(draggingPos.x-beginPos.x)*rotationSpeed);
        // Vector2 lookinput = (draggingPos - beginPos) * rotationSpeed * Time.deltaTime;
        // cameraPitch = Mathf.Clamp(cameraPitch - lookinput.y, 10f, 35f);
        // camPivot.localRotation = Quaternion.Euler(cameraPitch,0,0);


        yAngle = yAngleTmp + (draggingPos.x - beginPos.x) * Screen.height/622 * rotationSpeed * Time.deltaTime;
        xAngle = xAngleTmp - (draggingPos.y - beginPos.y) * Screen.width/1439 * rotationSpeed * Time.deltaTime;
        if (xAngle > max_xAngle)
        {
            Debug.Log("MAX_X");
            xAngle = max_xAngle;
        }
        if (xAngle < min_xAngle)
        {            
            Debug.Log("MAX_Y");
            xAngle = min_xAngle;
        }
        
        camPivot.rotation = Quaternion.Euler(xAngle,yAngle, 0.0f);
    }
}
