using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorRotate : MonoBehaviour
{
    [SerializeField]
    private bool rotateClockWise;
    [SerializeField]
    private float rotateSpeed;

    private int rotateDirection = 1;

    private void Start()
    {
        if (!rotateClockWise)
        {
            rotateDirection = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50*rotateSpeed *rotateDirection* Time.deltaTime, 0); 
    }
}
