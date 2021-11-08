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

        SPECIALSKILL_SPECIALIZED_FOR_CLEANING,  // ���|����
        SPECIALSKILL_OIL_COLLECTION,            // �����
        SPECIALSKILL_PLASTIC_ONLY,              // �v���X�`�b�N��p
        SPECIALSKILL_LARGE_CAPACITY_BATTERY,    // �o�b�e���[������(��)
    }



    // �����o�ϐ�
    [SerializeField, Range(0, 5)]
    public int clean, battery;              // �p�����[�^(clean:���|, battery:�쓮����)(0�`5:E�`S)

    public SPECIALSKILL_LIST specialSkill;  // ����Z�\

    public Image robotImage;                // ���{�b�g�̉摜
    


    // �p�����[�^�����֐�
    public void ParamGenerator()
    {
        // ���|
        float cleanAverage = (CharacterManager.Instance.SelectedCharacter[0].GetComponent<CharacterBase>().research + CharacterManager.Instance.SelectedCharacter[1].GetComponent<CharacterBase>().research) / 2;
        clean = Mathf.CeilToInt(cleanAverage);

        // �쓮����
        float batteryAverage = (CharacterManager.Instance.SelectedCharacter[0].GetComponent<CharacterBase>().management + CharacterManager.Instance.SelectedCharacter[1].GetComponent<CharacterBase>().management) / 2;
        battery = Mathf.CeilToInt(batteryAverage);
    }

    // ����Z�\�����֐�
    public void SpecialSkillGenerator()
    {
        CharacterBase.TAG_LIST tag1, tag2;
        tag1 = CharacterManager.Instance.SelectedCharacter[0].GetComponent<CharacterBase>().tag;
        tag2 = CharacterManager.Instance.SelectedCharacter[1].GetComponent<CharacterBase>().tag;

        if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_CLEANING || tag1 == CharacterBase.TAG_LIST.TAG_CLEANING && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)                               // ���{�b�g�H�w & ���|
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_SPECIALIZED_FOR_CLEANING; // ���|����
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_FUELOIL_COLLECTION || tag1 == CharacterBase.TAG_LIST.TAG_FUELOIL_COLLECTION && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)      // ���{�b�g�H�w & �d�����
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION;           // �����
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_PLASTIC_RESEARCH || tag1 == CharacterBase.TAG_LIST.TAG_PLASTIC_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)          // ���{�b�g�H�w & �v���X�`�b�N����
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY;             // �v���X�`�b�N��p
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_BATTERY_MANUFACTURE || tag1 == CharacterBase.TAG_LIST.TAG_BATTERY_MANUFACTURE && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)    // ���{�b�g�H�w & �o�b�e���[����
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY;   // �o�b�e���[������(��)
        }
        else
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_NULL;                     // ����Z�\����
        }   
    }



    // Start is called before the first frame update
    protected void Start()
    {
        ParamGenerator();
        SpecialSkillGenerator();

        // �f�o�b�O�p
        Debug.Log("���|�F" + RobotManager.Instance.RankTransfer(clean) + "  |  Parameter�F" + clean);
        Debug.Log("�쓮���ԁF" + RobotManager.Instance.RankTransfer(battery) + "  |  Parameter�F" + battery);
        Debug.Log("����Z�\�F" + specialSkill);
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
}
