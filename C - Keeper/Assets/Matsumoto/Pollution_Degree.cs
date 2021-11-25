using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Pollution_Degree : MonoBehaviour
{

    Slider pollutionSlider;
    IslandManager islandManager;
    float maxDegree = 100.0f;   //�򉻓x100%
    float nowDegree = 50.0f;     //���݂̏򉻓x

    void Start()
    {
        pollutionSlider = GetComponent<Slider>();

        //�򉻓x�̍ő�l�̐ݒ�
        pollutionSlider.maxValue = maxDegree;

    }


    void Update()
    {
        islandManager.totalPollutionLevel = pollutionSlider.value;
    }


    /*=====================Debug=======================*/
    public void Method()
    {
        Debug.Log("���ݒl�F" + pollutionSlider.value);

        if (pollutionSlider.value >= 100)
        {
            Debug.Log("�򉻂���܂����B");
        }

    }
    /*=================================================*/
}