using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FnishLineTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform firstFinishPos;
    private int finishedOpponentCount = 0;

    private int posX;
    private int posY;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            GameManager.instance.PlayerFinishedRace();
            //player won effects
        }
        else
        {
            Vector3 opponentWaitPos = firstFinishPos.position;
            if(finishedOpponentCount % 2 == 1)
            {
                opponentWaitPos.x = -opponentWaitPos.x;
            }
            opponentWaitPos.z = opponentWaitPos.z - (finishedOpponentCount / 2)*1.2f;
            other.gameObject.GetComponent<OpponentBasicAI>().FinishRace(opponentWaitPos);
            finishedOpponentCount++;
        }
    }
}
