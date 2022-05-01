using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStickForce : MonoBehaviour
{
    [SerializeField]
    private float pushForce;

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 dir = collision.contacts[0].point - collision.gameObject.transform.position;
        dir.y = 0;
        dir = -dir.normalized;
        collision.gameObject.GetComponent<PlayerController>().characterCanFall();
        collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * pushForce *10000, ForceMode.Force);
        Debug.Log("Collision oldu vectoru:" + dir);
       
    }
}
