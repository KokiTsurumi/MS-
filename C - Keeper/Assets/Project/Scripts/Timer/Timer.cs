using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public delegate void CallBack();
    private CallBack callBack;

    Image Gage;

    [SerializeField]
    float countTime;

    private bool start;

    [SerializeField] float currentTime;

    void Start()
    {
        currentTime = countTime;
        Gage = transform.GetChild(1).GetComponent<Image>();
    }

    //ÉQÅ[ÉÄàÍéûí‚é~Ç≈é~Ç‹ÇÈÇÊÇ§Ç…Ç∑ÇÈÇÃÇ≈FixedUpdate
    void FixedUpdate()
    {
        if (!start) return;
        //WorldManager.Instance

        currentTime -= Time.deltaTime;

        Gage.fillAmount = currentTime / countTime;

        if(currentTime <=0 )
        {
            start = false;
            this.gameObject.SetActive(false);
            if (callBack != null)
                callBack();
        }
        
    }

    public void TimerStart(float setTime)
    {
        start = true;
        currentTime = countTime = setTime;
    }

    public void TimerStart(float setTime, CallBack cb)
    {
        start = true;
        currentTime = countTime = setTime;
        callBack = cb;
    }

    public void TimerStart(float setTime, CallBack cb1, CallBack cb2)
    {
        start = true;
        currentTime = countTime = setTime;
        callBack = cb1 + cb2;
    }

    public float GetCurrentTime() => currentTime;

}
