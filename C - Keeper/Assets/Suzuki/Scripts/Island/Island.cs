using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Island : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int pollutionLevel = 100;   // �����x(0%�`100%)
    [SerializeField] private bool checkInvestigated = false;            // ���̒������������Ă��邩�̃t���O
    [SerializeField] private Text pollutionLevelText;                   // ���̉����x��\�����邽�߂�Text

    // ���������ς��ǂ�����Ԃ��֐�
    public bool GetCheckInvastigate()
    {
        return checkInvestigated;
    }

    // ���������t���O��true�ɂ���֐�
    public void CompleteInvastigate()
    {
        checkInvestigated = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkInvestigated)// ������
            pollutionLevelText.text = "�C�m�����x�F" + pollutionLevel.ToString() + "%";
        else// ������
            pollutionLevelText.text = "�C�m�����x�F---%";
    }
}
