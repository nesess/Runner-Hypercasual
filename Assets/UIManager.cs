using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private PaintIn3D.P3dChannelCounter paintWallCounter;

    [SerializeField]
    private GameObject paintScene;

    [SerializeField]
    private Image fillImage;

   
    [SerializeField]
    private TextMeshProUGUI percentageText;






    // Start is called before the first frame update
    void Start()
    {

       
        StartCoroutine(PaintPercentageRoutine());

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
    

}
