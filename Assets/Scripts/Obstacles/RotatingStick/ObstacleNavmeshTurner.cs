using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleNavmeshTurner : MonoBehaviour
{
    private MoveHorizontal moveHorizontal;
    private GameObject myParent;



    private void Start()
    {
        myParent = transform.parent.gameObject;
        moveHorizontal = myParent.GetComponentInChildren<MoveHorizontal>();
    }


}
