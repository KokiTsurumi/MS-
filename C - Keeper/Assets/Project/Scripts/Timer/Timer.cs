using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    
    Image Gage;

    [SerializeField]
    float countTime;

    private bool start;

    float currentTime;

    void Start()
    {
        currentTime = countTime;
        Gage = transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        if (!start) return;


        currentTime -= Time.deltaTime;

        Gage.fillAmount = currentTime / countTime;

        if(currentTime <=0 )
        {
            start = false;
        }
        
    }

    public void TimerStart(float setTime)
    {
        start = true;
        currentTime = countTime = setTime;
    }

    public float GetCurrentTime() => currentTime;

}
