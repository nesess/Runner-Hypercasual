using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rigid;
    private Animator anim;

    private Vector3 lastMousePos;

    [SerializeField]
    private float bounds, sensivitiy, clampDelta;

    [SerializeField]
    private float speed;

    private bool canMove = false,fail = true,falling;

    private bool rotPlatform = false;
    private float  platformMoveDirection;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == false && Input.GetMouseButton(0) && fail)
        {
            anim.SetTrigger("Run");
            canMove = true;
            fail = false;
            
        }

        if(!falling && Mathf.Abs(transform.position.x) < bounds + 0.5)
        {
            
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -bounds, bounds), transform.position.y, transform.position.z);
        }
        
    }

 

    private void FixedUpdate()
    {
        if (canMove && !falling)
        {
            Vector3 moveVector = new Vector3(rigid.velocity.x, rigid.velocity.y, speed);
            rigid.velocity = moveVector;
        }

        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
        }


        if (Input.GetMouseButton(0) && canMove && !falling)
        {
            Vector3 vector = lastMousePos - Input.mousePosition;
            lastMousePos = Input.mousePosition;
            vector = new Vector3(vector.x, 0, 0);

            Vector3 moveForce = Vector3.ClampMagnitude(vector, clampDelta);
            Vector3 minusVec = rigid.velocity;
            minusVec.y = 0;
            rigid.AddForce(-moveForce * sensivitiy - minusVec / 5, ForceMode.VelocityChange);

        }
        else if(!Input.GetMouseButton(0) && !falling)
        {
            Vector3 stopVector = new Vector3(rigid.velocity.x * 0.8f, rigid.velocity.y, rigid.velocity.z);
            rigid.velocity = stopVector;
        }

        if(rotPlatform)
        {
            Vector3 moveVector = new Vector3(platformMoveDirection/15, 0, 0);
            rigid.AddForce(moveVector,ForceMode.VelocityChange);
         
        }

        rigid.velocity.Normalize();


    }

    public void rotatingPlatformMove(bool state,float moveDir)
    {
        
        rotPlatform = state;
        platformMoveDirection = moveDir;
        
    }

    public void playerFailed()
    {
        if(canMove)
        {
            canMove = false;
            anim.SetTrigger("Fail");
            StartCoroutine(failAnimCoroutine());
        }
    }

    public void playerDead()
    {
       
            canMove = false;
            anim.SetTrigger("Fail");
            anim.SetTrigger("Idle");
            transform.position = GameObject.Find("StartPos").transform.position;
            fail = true;
       
    }

    public void characterCanFall()
    {
        
        StartCoroutine(canFallCooutine());
    }

    private IEnumerator canFallCooutine()
    {
        falling = true;
        yield return new WaitForSeconds(0.5f);
        falling = false;
    }

    private IEnumerator failAnimCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        anim.SetTrigger("Idle");
        transform.position = GameObject.Find("StartPos").transform.position;
        fail = true;
    }

}
