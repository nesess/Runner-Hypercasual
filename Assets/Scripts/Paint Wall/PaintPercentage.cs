using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintPercentage : MonoBehaviour
{


    public Texture2D texture;
    private int totalRed;

    public bool test = false;

    private void Awake()
    {
       
    }

    private void Start()
    {
        texture = new Texture2D(128, 128);
        GetComponent<Renderer>().material.mainTexture = texture;
        
    }


    void Update()
    {
     
        if(test)
        {
            for (int y = 0; y < texture.height; y++)
            {
                for (int x = 0; x < texture.width; x++)
                {
                    Debug.Log("regi vermesi lazým: " + texture.GetPixel(x, y));
                }
            }
            test = false;
        }
    }

    private IEnumerator checkPixels()
    {
        

        yield return new WaitForSeconds(0.1f);
    }

}
