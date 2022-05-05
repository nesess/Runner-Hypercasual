using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentBasicAI : Character
{

    private NavMeshAgent agent;
    private NavMeshPath currentPath;
    private bool startedFirstTime = false;

    [SerializeField]
    private Transform targetPos;
    [SerializeField]
    private float horizontalMoveForce;
    [SerializeField]
    private float AIPathCalculationFrequency = 0.1f;


    private void Start()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
        
        currentPath = new NavMeshPath();
        StartCoroutine(calculatePathRoutine());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(startedFirstTime && canMove == false && fail)
        {
            rigid.mass = 50;
            anim.SetTrigger("Run");
            canMove = true;
            fail = false;
        }
        else if(canMove == false && fail && Input.GetMouseButton(0))
        {
            startedFirstTime = true;
            anim.SetTrigger("Run");
            canMove = true;
            fail = false;
        }

    }

    private IEnumerator calculatePathRoutine()
    {
        
        while(Vector3.Distance(transform.position,targetPos.position)>2)
        {
            try
            {
                if(canMove && !fail && Mathf.Abs(transform.position.x) < bounds + 0.2)
                {
                    agent.gameObject.SetActive(false);
                    agent.gameObject.transform.position = transform.position;
                    agent.gameObject.SetActive(true);
                    currentPath = new NavMeshPath();
                    if (agent.isActiveAndEnabled)
                    {
                        agent.CalculatePath(targetPos.position, currentPath);
                        Debug.DrawLine(currentPath.corners[0], currentPath.corners[1], Color.red, 0.1f, false);
                    }
                }
            }
            catch(Exception e)
            {
                Debug.Log("e:" + e +"my name: " + gameObject.name.ToString());
            }
            yield return new WaitForSecondsRealtime(AIPathCalculationFrequency);
        }
    }
    
    


    protected override void FixedUpdate()
    {

        
        base.FixedUpdate();

        
        if (currentPath != null && !agent.pathPending && currentPath.corners.Length > 0 && canMove && !falling && Mathf.Abs(transform.position.x) < bounds + 0.2 && currentPath.corners.Length > 2)
        {


            Vector3 moveVec = currentPath.corners[1] - transform.position;
            moveVec.x = moveVec.x*horizontalMoveForce;
            moveVec.z = rigid.velocity.z;
            moveVec.y = rigid.velocity.y;
            rigid.velocity = moveVec;

        }
    }

    public void FinishRace(Vector3 pos)
    {
        canMove = false;
        StartCoroutine(MoveFinishPoint(pos));
    }

    private IEnumerator MoveFinishPoint(Vector3 pos)
    {
        Vector3 startingPos = transform.position;
        float elapsedTime = 0;
        rigid.useGravity = false;
        rigid.isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;
        pos.y = transform.position.y;

        while (elapsedTime < 1.0f)
        {
            transform.position = Vector3.Lerp(startingPos, pos, (elapsedTime / 1.0f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Vector3 endingRot = new Vector3(0, -90, 0);
        if (pos.x<0)
        {
            endingRot.y = 90;
        }
        transform.eulerAngles = endingRot;
        anim.SetTrigger("Dance");
        
    }


}
