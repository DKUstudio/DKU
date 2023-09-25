using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float speed = 10f;
    public float jump = 3f;
    public float dash = 5f;
    public float rotSpeed = 3f;

    private Vector3 dir = Vector3.zero;

    private bool ground = false;
    public LayerMask layer;
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
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
        if (dir != Vector3.zero)
        {
            if (Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x) || Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
            {
                transform.Rotate(0,1,0);
            }
            transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.deltaTime);
        }
        
        _rigidbody.MovePosition(this.gameObject.transform.position + dir * speed * Time.deltaTime);
    }

    void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f),Vector3.down,out hit,0.4f,layer))
        {
            ground = true;
        }
        else
        {
            ground = false;
        }
    }
}
