using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject mainCam;
    [SerializeField]
    private GameObject paintWallCam;

    [SerializeField]
    private GameObject opponentContainer;
    private int opponentCount;
    private int playerRank = 1;
    private bool playerFinished = false;

    [SerializeField]
    private GameObject paintableObj;
    [SerializeField]
    private GameObject confetti;

    [SerializeField]
    private Transform playerPaintPos;

    private void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //Set screen size for Standalone
        #if UNITY_STANDALONE
            Screen.SetResolution(564, 960, false);
            Screen.fullScreen = false;
        #endif

        opponentCount = opponentContainer.transform.childCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void LateUpdate()
    {
        if(!playerFinished)
        {
            int playerLastRank = playerRank;
            playerRank = 1;
            for (int i = 0; i < opponentCount; i++)
            {
                if (player.transform.position.z < opponentContainer.transform.GetChild(i).transform.position.z)
                {
                    playerRank++;
                }
            }

            if (playerLastRank != playerRank)
            {
                UIManager.instance.changePlayerRank(playerRank);
            }
        }
       
    }

    public int getOpponentCount()
    {
        return opponentCount+1;
    }

    public void PlayerFinishedRace()
    {
        playerFinished = true;
        
        confetti.SetActive(true);

    }

    public Vector3 GetSpawnPos()
    {
        Vector3 spawnPoint;
        int watcher = 0;
        while (true)
        {
            watcher++;
            if(watcher >50)
            {
                return Vector3.zero;
            }
            float randomX = Random.Range(-2f, 2f);
            float randomZ = Random.Range(-7.2f, -8.6f);
            spawnPoint = new Vector3(randomX, 0.4f, randomZ);
            Collider[] hitColliders = new Collider[2];
            int collideCount = Physics.OverlapSphereNonAlloc(spawnPoint, 1.0f,hitColliders,LayerMask.GetMask("Character"));
            if (!(collideCount > 0))
            {
          
                return spawnPoint;
            }
        } 
    }
    

    public void PlayerReachedPaint()
    {
        opponentContainer.SetActive(false);
        mainCam.SetActive(false);
        paintWallCam.SetActive(true);
        player.transform.position = playerPaintPos.position;
        player.GetComponentInChildren<Animator>().SetTrigger("Idle");
        player.GetComponent<PlayerController>().enabled = false;
        UIManager.instance.StartPaintScene();
    }

    public void paintTimeFinished()
    {
        paintableObj.SetActive(false);
        for (int i = 0; i < opponentCount; i++)
        {
            opponentContainer.transform.GetChild(i).GetComponent<Character>().Stop();
            
        }
        UIManager.instance.SwitchEndScene(playerRank);
        //paint süresi bitti son yüzde ve rank gösterimi olan scene
    }

  

}
