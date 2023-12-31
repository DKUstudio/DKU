using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public Transform camPivot;
    private Rigidbody _rigidbody;
    private Animator _animation;
    public float speed = 10f;
    public float jump = 3f;
    public float dash = 5f;
    public float rotSpeed = 3f;
    public string animName;
    public string LastanimName;
    private int modelNUM;
    private int modelCount;
    private Vector3 dir = Vector3.zero;
    private float DashCool = 2f;
    public bool ground = false;
    public bool ismove = false;
    public LayerMask layer;
    void Start()
    {
        // modelNUM <- 서버에서 받아온 정보
        //modelNUM = PlayerInfo.instance.bitshift;
        modelNUM = NetworkManager.Instance.UDATA.charaShift;
        _rigidbody = this.GetComponent<Rigidbody>();
        modelCount = transform.childCount;
        animName = "Idle_A";
        LastanimName = "Idle_A";
        ChangeModel(modelNUM);

    }

    void Update()
    {
        // dir.x = Input.GetAxis("Horizontal");
        // dir.z = Input.GetAxis("Vertical");
        dir.x = joystick.Horizontal;
        dir.z = joystick.Vertical;

        dir.Normalize();

        CheckGround();
        if (!ground)
        {
            animName = "Fly";
        }
        else if (ismove)
        {
            animName = "Walk";
        }
        else
        {
            animName = "Idle_A";
        }
        
        if (LastanimName != animName)
        {
            LastanimName = animName;
            MemberService.AnimChanged(animName);
            _animation.Play(animName);
        }

        if (DashCool > 0)
        {
            DashCool -= Time.deltaTime;
        }
        
        if (Input.GetButtonDown("Jump") && ground)
        {
            Vector3 jumpPower = Vector3.up * jump;
            _rigidbody.AddForce(jumpPower, ForceMode.VelocityChange);
        }
        if (Input.GetButtonDown("Dash"))
        {
            Vector3 dashPower = this.transform.forward * dash;
            _rigidbody.AddForce(dashPower, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        Vector2 conDir = joystick.Direction;
        if (conDir == Vector2.zero)
        {
            ismove = false;
            return;
        }

        ismove = true;

        float thetaEuler = Mathf.Acos(conDir.y / conDir.magnitude) * (180 / Mathf.PI) * Mathf.Sign(conDir.x);
        Vector3 moveAngle = Vector3.up * (camPivot.transform.rotation.eulerAngles.y + thetaEuler);
        transform.rotation = Quaternion.Euler(moveAngle);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        // _rigidbody.MovePosition(this.gameObject.transform.position + dir * speed * Time.deltaTime);
    }

    void CheckGround()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + (Vector3.up * 0.3f), Vector3.down, Color.red, 1.0f);
        if (Physics.Raycast(transform.position + (Vector3.up * 0.3f), Vector3.down, out hit, 1.0f, layer))
        {
            ground = true;
        }
        else
        {
            ground = false;
        }
    }
    [Button]
    public void ChangeModel(int n)
    {
        modelNUM = n;
        for (int i = 0; i < modelCount; i++)
        {
            if (i == n)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        _animation = transform.GetChild(modelNUM).GetComponent<Animator>();
    }

    public void JUMP()
    {
        if (ground)
        {
            Vector3 jumpPower = Vector3.up * jump;
            _rigidbody.AddForce(jumpPower, ForceMode.VelocityChange);
        }
    }

    public void DASH()
    {
        if (DashCool <= 0)
        {
            DashCool = 2f;
            Vector3 dashPower = this.transform.forward * dash;
            _rigidbody.AddForce(dashPower, ForceMode.VelocityChange);
        }
    }
}
