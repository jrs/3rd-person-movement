using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private bool _fadeIn;
    private bool _fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        if(_fadeIn)
        {
            if(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime;
                if(canvasGroup.alpha >= 1)
                {
                    _fadeIn = false;
                }
            }
        }

        if(_fadeOut)
        {
            if(canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= Time.deltaTime;
                if(canvasGroup.alpha == 0)
                {
                    _fadeOut = false;
                    //GameObject.Find("Game Manager").GetComponent<Timer>().StartGameTimer();
                }
            }
        }
    } // end Update

    public void FadeIn()
    {
        _fadeIn = true;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void FadeOut()
    {
        _fadeOut = true;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
