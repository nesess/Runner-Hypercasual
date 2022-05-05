using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController  : Character
{

   

    private Vector3 lastMousePos;

    [SerializeField]
    private float  sensivitiy, clampDelta;

    

    

   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (canMove == false && Input.GetMouseButton(0) && fail)
        {
            UIManager.instance.startHandSetFalse();
            rigid.mass = 50;
            anim.SetTrigger("Run");
            canMove = true;
            fail = false;
            
        }

        
        
    }

 

    protected override void FixedUpdate()
    {

        base.FixedUpdate();

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

       

        rigid.velocity.Normalize();


    }

    

}
