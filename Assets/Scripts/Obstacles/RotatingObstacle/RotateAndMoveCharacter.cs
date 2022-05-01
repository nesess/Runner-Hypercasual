using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndMoveCharacter : MonoBehaviour
{
    [SerializeField]
    private bool rotateClockWise;
    [SerializeField]
    private float moveForce;
    [SerializeField]
    private float rotateSpeed;

    private Vector3 moveVector;
    private int rotateDirection = 1;

    private void Start()
    {
        if(!rotateClockWise)
        {
            rotateDirection = -1;
        }

        moveVector = new Vector3(moveForce * rotateDirection, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0 , rotateSpeed* 50 * Time.deltaTime*rotateDirection*-1);
    }

    private void OnCollisionStay(Collision collision)
    {
        collision.gameObject.GetComponent<PlayerController>().rotatingPlatformMove(true, rotateDirection * moveForce);
    }


    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.GetComponent<PlayerController>().rotatingPlatformMove(false, rotateDirection * moveForce);
    }

}
