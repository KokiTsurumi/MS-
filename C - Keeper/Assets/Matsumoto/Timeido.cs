using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeido : MonoBehaviour
{

    public Text text;

    // Use this for initialization
    void Start()
    {
        //�ŏ��̃����N
        text.text = "�����N1";
    }

    // Update is called once per frame
    void Update()
    {
        //�m���x�������胉���N�ɕύX�������
        if (Input.GetMouseButtonDown(0))
        {
            text.text = "�����N2";
        }
    }

}