using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Pollution_Degree : MonoBehaviour
{

    Slider pollutionSlider;
    IslandManager islandManager;
    float maxDegree = 100.0f;   //浄化度100%
    float nowDegree = 50.0f;     //現在の浄化度

    void Start()
    {
        pollutionSlider = GetComponent<Slider>();

        //浄化度の最大値の設定
        pollutionSlider.maxValue = maxDegree;

    }


    void Update()
    {
        islandManager.totalPollutionLevel = pollutionSlider.value;
    }


    /*=====================Debug=======================*/
    public void Method()
    {
        Debug.Log("現在値：" + pollutionSlider.value);

        if (pollutionSlider.value >= 100)
        {
            Debug.Log("浄化されました。");
        }

    }
    /*=================================================*/
}