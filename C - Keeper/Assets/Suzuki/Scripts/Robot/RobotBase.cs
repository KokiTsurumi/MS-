using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotBase : MonoBehaviour
{
    // ����Z�\
    public enum SPECIALSKILL_LIST
    {
        SPECIALSKILL_NULL,

        SPECIALSKILL_SPECIALIZED_FOR_CLEANING,
        SPECIALSKILL_OIL_COLLECTION,
        SPECIALSKILL_PLASTIC_ONLY,
        SPECIALSKILL_LARGE_CAPACITY_BATTERY,
    }



    // �����o�ϐ�
    public int clean, battery;              // �p�����[�^(clean:���|, battery:�쓮����)(0�`5:E�`S)
    public SPECIALSKILL_LIST specialSkill;  // ����Z�\

    public Image robotImage;                // ���{�b�g�̉摜

    public GameObject CharacterManager;     // CharacterManager�擾�p

    // �p�����[�^�����֐�
    public void ParamGenerator()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
