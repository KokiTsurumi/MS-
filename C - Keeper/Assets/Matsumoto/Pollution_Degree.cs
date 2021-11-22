using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Pollution_Degree : MonoBehaviour
{

    Slider pollutionSlider;

    void Start()
    {

        pollutionSlider = GetComponent<Slider>();

         float maxDegree = 100.0f;   //浄化度100%
         float nowDegree = 0.0f;     //現在の浄化度


        //浄化度の最大値の設定
        pollutionSlider.maxValue = maxDegree;

        //現在値の設定
        pollutionSlider.value = nowDegree;


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