using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Character : MonoBehaviour
{
    protected Rigidbody rigid;
    protected Animator anim;


    

    [SerializeField]
    private float speed;
    [SerializeField]
    protected float bounds;

    [SerializeField]
    protected bool canMove = false, fail = true, falling;

    private bool rotPlatform = false;
    private float platformMoveDirection;
    private CapsuleCollider myCollider;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        myCollider = GetComponent<CapsuleCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!falling && Mathf.Abs(transform.position.x) < bounds + 0.4)
        {

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -bounds, bounds), transform.position.y, transform.position.z);
        }
    }

    protected virtual  void FixedUpdate()
    {
        if (canMove && !falling)
        {
            Vector3 moveVector = new Vector3(rigid.velocity.x, rigid.velocity.y, speed);
            rigid.velocity = moveVector;
        }

        if (rotPlatform)
        {
            Vector3 moveVector = new Vector3(platformMoveDirection / 15, 0, 0);
            rigid.AddForce(moveVector, ForceMode.VelocityChange);

        }

        rigid.velocity.Normalize();
    }

    public void rotatingPlatformMove(bool state, float moveDir)
    {

        rotPlatform = state;
        platformMoveDirection = moveDir;

    }

    public void chracterFailed()
    {
        if (canMove)
        {
            canMove = false;
            Vector3 velo = rigid.velocity;
            velo.x = 0;
            rigid.velocity = velo;
            anim.SetTrigger("Fail");
            StartCoroutine(failAnimCoroutine());
        }
    }

    public void characterDead()
    {

        canMove = false;
        anim.SetTrigger("Fail");
        anim.SetTrigger("Idle");
        rigid.mass = 1000;
        transform.position = GameManager.instance.GetSpawnPos();
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

    public void Stop()
    {
        canMove = false;
    }

    private IEnumerator failAnimCoroutine()
    {
       
        yield return new WaitForSeconds(3.0f);
        anim.SetTrigger("Idle");
        rigid.mass = 1000;
        transform.position = GameManager.instance.GetSpawnPos();
        fail = true;
    }
}
