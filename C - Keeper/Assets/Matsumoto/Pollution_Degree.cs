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

         float maxDegree = 100.0f;   //�򉻓x100%
         float nowDegree = 0.0f;     //���݂̏򉻓x


        //�򉻓x�̍ő�l�̐ݒ�
        pollutionSlider.maxValue = maxDegree;

        //���ݒl�̐ݒ�
        pollutionSlider.value = nowDegree;


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