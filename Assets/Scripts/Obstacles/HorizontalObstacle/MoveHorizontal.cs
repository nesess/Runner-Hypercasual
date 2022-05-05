using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontal : MonoBehaviour
{
    public float speed, distance;
    private float minX, maxX;

    public bool right, dontMove;
    private bool stop;

    private GameObject navmeshObstacle;

    // Start is called before the first frame update
    void Start()
    {
        navmeshObstacle = transform.GetChild(0).gameObject;
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
                    Quaternion newRot = navmeshObstacle.transform.rotation;
                    newRot.y = 0;
                    navmeshObstacle.transform.rotation = newRot;
                    right = false;
                }
            }
            else
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                if (transform.position.x <= minX)
                {
                    Quaternion newRot = navmeshObstacle.transform.rotation;
                    newRot.y = 180;
                    navmeshObstacle.transform.rotation = newRot;
                    right = true;
                }
            }
        }
       
    }
}
