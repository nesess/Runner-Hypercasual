using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private PaintIn3D.P3dChannelCounter paintWallCounter;

    [SerializeField]
    private GameObject gameScene;

    [SerializeField]
    private GameObject paintScene;

    [SerializeField]
    private GameObject endScene;

    [SerializeField]
    private Image fillImage;

   
    [SerializeField]
    private TextMeshProUGUI percentageText;

    [SerializeField]
    private TextMeshProUGUI playerPositionText;

    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private float paintTime=10.0f;

    [SerializeField]
    private TextMeshProUGUI endRankText;
    [SerializeField]
    private TextMeshProUGUI endPercentageText;

    [SerializeField]
    private GameObject startHand;

    public static UIManager instance;

    
   

    private int opponentCount;

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
    }


    // Start is called before the first frame update
    void Start()
    {
        opponentCount = GameManager.instance.getOpponentCount();
        paintScene.SetActive(false);
        endScene.SetActive(false);
        paintWallCounter.gameObject.SetActive(false);
        

    }

    public void StartPaintScene()
    {
        gameScene.SetActive(false);
        paintWallCounter.gameObject.SetActive(true);
        paintScene.SetActive(true);
        
        StartCoroutine(PaintPercentageRoutine());
        StartCoroutine(TimeCounter());
    }

    public void changePlayerRank(int rank)
    {
        playerPositionText.text = rank + "/" + opponentCount;
    }

   
    public void startHandSetFalse()
    {
        startHand.SetActive(false);
    }

    private IEnumerator TimeCounter()
    {
        while(paintTime >0)
        {
            paintTime -= 0.1f;
            timeText.text = paintTime.ToString("f1");
            yield return new WaitForSecondsRealtime(0.1f);
        }
        GameManager.instance.paintTimeFinished();
        
    }


    private  IEnumerator PaintPercentageRoutine()
    {
        while(true)
        {
            
            fillImage.fillAmount = 1 - paintWallCounter.RatioG;
            percentageText.text = "%" + ((1f - paintWallCounter.RatioG) * 100).ToString("f0");
            yield return new WaitForFixedUpdate();
        }
    }

    public void SwitchEndScene(int rank)
    {
        paintScene.SetActive(false);
        endScene.SetActive(true);
        endRankText.text = "Your rank: " + rank + "/11";
        endPercentageText.text = "Wall paýntýng percentage: %" + ((1f - paintWallCounter.RatioG) * 100).ToString("f0");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
