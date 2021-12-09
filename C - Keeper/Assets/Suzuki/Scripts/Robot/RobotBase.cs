using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotBase : MonoBehaviour
{
    /// <summary>
    /// ����Z�\
    /// </summary>
    public enum SPECIALSKILL_LIST
    {
        SPECIALSKILL_NULL,

        SPECIALSKILL_SPECIALIZED_FOR_CLEANING,  // ���|����
        SPECIALSKILL_OIL_COLLECTION,            // �����
        SPECIALSKILL_PLASTIC_ONLY,              // �v���X�`�b�N��p
        SPECIALSKILL_LARGE_CAPACITY_BATTERY,    // ��e�ʃo�b�e���[
        SPECIALSKILL_PERFECT_INVESTIGATOR,      // ���������@
    }



    // �����o�ϐ�
    [SerializeField, Range(0, 5)]
    public int clean, battery;              // �p�����[�^(clean:���|, battery:�쓮����)(0�`5:E�`S)

    public SPECIALSKILL_LIST specialSkill;  // ����Z�\
    public string name;                     // ���O

    public Sprite robotSprite;              // ���{�b�g�̉摜



    /// <summary>
    /// �e��p�����[�^�����֐�
    /// </summary>
    public void ParamGenerator()
    {
        // ���|
        float cleanAverage = (CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().research + CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().research) / 2;
        clean = Mathf.CeilToInt(cleanAverage);

        // �쓮����
        float batteryAverage = (CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().management + CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().management) / 2;
        battery = Mathf.CeilToInt(batteryAverage);
    }

    /// <summary>
    /// ����Z�\�����֐�
    /// </summary>
    public void SpecialSkillGenerator()
    {
        CharacterBase.TAG_LIST tag1, tag2;
        tag1 = CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().tag;
        tag2 = CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().tag;

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
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY;   // ��e�ʃo�b�e���[
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH)                                                                                                          // ���R������ & ���R������
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_PERFECT_INVESTIGATOR;     // ���������@
        }
        else
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_NULL;                     // ����Z�\����
        }   
    }

    /// <summary>
    /// ���O�����֐�
    /// </summary>
    public void NameGenerator()
    {
        string name1, name2;

        // �@�̖�
        if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_SPECIALIZED_FOR_CLEANING)    // ���|����
            name1 = "CHS�E";
        else if(specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION)          // �����
            name1 = "OCO�E";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY)           // �v���X�`�b�N��p
            name1 = "PCO�E";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY) // ��e�ʃo�b�e���[
            name1 = "LCB�E";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_PERFECT_INVESTIGATOR)   // ���������@
            name1 = "PNS�E";
        else                                                                            // ����Z�\����
            name1 = "IHN�E";

        // ���㖼
        if (clean <= 0)             // E
            name2 = "Prototype";
        else if (clean == 1)        // D
            name2 = "Mk.I";
        else if (clean == 2)        // C
            name2 = "Mk.II";
        else if (clean == 3)        // B
            name2 = "Mk.III";
        else if (clean == 4)        // A
            name2 = "Mk.IV";
        else                        // S
            name2 = "ULTIMATE";

        // ���O����
        name = name1 + name2;
    }



    // Start is called before the first frame update
    protected void Start()
    {
        ParamGenerator();
        SpecialSkillGenerator();
        NameGenerator();

        // �f�o�b�O�p
        Debug.Log("���|    �F" + RobotManager.Instance.RankTransfer(clean) + "  |  Parameter�F" + clean);
        Debug.Log("�쓮���ԁF" + RobotManager.Instance.RankTransfer(battery) + "  |  Parameter�F" + battery);
        Debug.Log("����Z�\�F" + specialSkill);
        Debug.Log("���O    �F" + name);
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
}
