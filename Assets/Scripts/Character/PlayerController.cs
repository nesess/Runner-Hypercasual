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

    private bool canMove = false;

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
        if (canMove == false && Input.GetMouseButton(0))
        {
            anim.SetTrigger("Run");
            canMove = true;
            rigid.velocity = new Vector3(rigid.velocity.x, 0, speed);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -bounds, bounds), transform.position.y, transform.position.z);

    }

    private void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
        }


        if (Input.GetMouseButton(0) && canMove)
        {
            Vector3 vector = lastMousePos - Input.mousePosition;
            lastMousePos = Input.mousePosition;
            vector = new Vector3(vector.x, 0, 0);

            Vector3 moveForce = Vector3.ClampMagnitude(vector, clampDelta);
            rigid.AddForce(-moveForce * sensivitiy - rigid.velocity / 5, ForceMode.VelocityChange);

        }
        else if(!Input.GetMouseButton(0))
        {
            Vector3 stopVector = new Vector3(rigid.velocity.x * 0.8f, 0, rigid.velocity.z);
            rigid.velocity = stopVector;
        }

        rigid.velocity.Normalize();


    }
}
