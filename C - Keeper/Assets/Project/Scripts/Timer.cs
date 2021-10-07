using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image Gage;
    public bool roop;
    public float countTime;

    private bool start;

    public GameObject timer;

    // Start is called before the first frame update
    void Start()
    {
        timer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!start) return;

        if (roop)
            Gage.fillAmount -= 1.0f / countTime * Time.deltaTime;

        if (Gage.fillAmount <= 0)
        {
            Debug.Log("owata");
        }
    }

    public void TimerStart()
    {
        //•b”‚Æ‚©ˆø”‚ÉŽæ‚é‚ÆŠÈ’P
        timer.SetActive(true);
        start = true;
    }
}
