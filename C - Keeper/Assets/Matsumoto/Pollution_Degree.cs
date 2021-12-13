using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Pollution_Degree : MonoBehaviour
{

    Slider pollutionSlider;
    //IslandManager islandManager;
    float maxDegree = 100.0f;   //�򉻓x100%
    //float nowDegree = 50.0f;     //���݂̏򉻓x

    void Start()
    {
        pollutionSlider = GetComponent<Slider>();

        //�򉻓x�̍ő�l�̐ݒ�
        pollutionSlider.maxValue = maxDegree;
        pollutionSlider.value = 0;
    }


    void Update()
    {
        //islandManager.totalPollutionLevel = pollutionSlider.value;
        if(IslandManager.Instance.totalPollutionLevel != 0)
            pollutionSlider.value = 100 - IslandManager.Instance.totalPollutionLevel;
    }


    /*=====================Debug=======================*/
    public void Method()
    {
        //Debug.Log("���ݒl�F" + pollutionSlider.value);

        if (pollutionSlider.value >= 100)
        {
            //Debug.Log("�򉻂���܂����B");
        }

    }
    /*=================================================*/
}