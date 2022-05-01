using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontal : MonoBehaviour
{
    public float speed, distance;
    private float minX, maxX;

    public bool right, dontMove;
    private bool stop;

    // Start is called before the first frame update
    void Start()
    {

        maxX = transform.position.x + distance;
        minX = transform.position.x - distance;

    }

    private void FixedUpdate()
    {
        if (!stop && !dontMove)
        {
            if (right)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                if (transform.position.x >= maxX)
                {
                    right = false;
                }
            }
            else
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                if (transform.position.x <= minX)
                {
                    right = true;
                }
            }
        }
        transform.Rotate(0, 50 * Time.deltaTime,0 ); //rotates 50 degrees per second around z axis
    }
}
