using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public Transform camPivot;
    private Rigidbody _rigidbody;
    public float speed = 10f;
    public float jump = 3f;
    public float dash = 5f;
    public float rotSpeed = 3f;

    private Vector3 dir = Vector3.zero;

    public bool ground = false;
    public LayerMask layer;
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // dir.x = Input.GetAxis("Horizontal");
        // dir.z = Input.GetAxis("Vertical");
        dir.x = joystick.Horizontal;
        dir.z = joystick.Vertical;
        
        dir.Normalize();
        
        CheckGround();
        
        if (Input.GetButtonDown("Jump") && ground)
        {
            Vector3 jumpPower = Vector3.up * jump;
            _rigidbody.AddForce(jumpPower,ForceMode.VelocityChange);
        }
        if (Input.GetButtonDown("Dash"))
        {
            Vector3 dashPower = this.transform.forward * dash;
            _rigidbody.AddForce(dashPower,ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        Vector2 conDir = joystick.Direction;
        if (conDir == Vector2.zero)
        {
            return;
        }

        float thetaEuler = Mathf.Acos(conDir.y / conDir.magnitude) * (180 / Mathf.PI) * Mathf.Sign(conDir.x);
        Vector3 moveAngle = Vector3.up * (camPivot.transform.rotation.eulerAngles.y + thetaEuler);
        transform.rotation = Quaternion.Euler(moveAngle);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        // _rigidbody.MovePosition(this.gameObject.transform.position + dir * speed * Time.deltaTime);
    }

    void CheckGround()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + (Vector3.up * 0.3f),Vector3.down,Color.red,1.0f);
        if (Physics.Raycast(transform.position + (Vector3.up * 0.3f),Vector3.down,out hit,1.0f,layer))
        {
            ground = true;
        }
        else
        {
            ground = false;
        }
    }
}
