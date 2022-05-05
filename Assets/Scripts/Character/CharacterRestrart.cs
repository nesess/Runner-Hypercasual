using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRestrart : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            GetComponent<Character>().chracterFailed();
            
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            GetComponent<Character>().characterDead();
        }
    }
}
