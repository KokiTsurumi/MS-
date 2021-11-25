using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IslandBase : MonoBehaviour
{
    // ���������Ă���
    public enum PROBLEM_LIST
    {
        PROBLEM_TRASH,      // �S�~���
        PROBLEM_PLASTIC,    // �v���X�`�b�N���
        PROBLEM_FUELOIL,    // �d�����

        PROBLEM_MAX,        // ���̎�ނ̍ő吔
    }

    // ���̏��(�X�e�[�g�}�V��)
    public enum STATE_ISLAND
    {
        STATE_UNINVESTIGATED,   // ������
        STATE_INVESTIGATING,    // ������
        STATE_INVESTIGATED,     // ������
        STATE_CLEANING,         // ���|��
        STATE_CLEANED,          // ���|��
    }



    // �����o�ϐ�
    [SerializeField, Range(0, 100)] private int pollutionLevel = 100;   // �����x(0%�`100%)
    [SerializeField, Range(0, 5)] private int pollutionRank = 0;        // ���������N(0�`5:E�`S)
    [SerializeField] protected bool checkInvestigated = false;          // ���̒������������Ă��邩�̃t���O
    [SerializeField] protected Text pollutionLevelText;                 // ���̉����x��\�����邽�߂�Text

    int removeRate;                                                     // ������

    public PROBLEM_LIST problem;                                        // ���������Ă���
    public STATE_ISLAND state;                                          // ���̏��

    public GameObject timer;                                            // �^�C�}�[�I�u�W�F�N�g
    public GameObject icon;                                             // �r�b�N���}�[�N�A�C�R���I�u�W�F�N�g



    // ���̉����x���擾����֐�
    public int GetPollutionLevel()
    {
        return pollutionLevel;
    }

    // ���̏��������v�Z����֐�
    public int CalcRemoveRate()
    {
        RobotData robot1 = RobotManager.Instance.selectedRobot[0].GetComponent<RobotData>();    // ���{�b�g�P
        RobotData robot2 = RobotManager.Instance.selectedRobot[1].GetComponent<RobotData>();    // ���{�b�g�Q

        int cleanAverage = RobotManager.Instance.GetCleanAverage(robot1, robot2);               // ���{�b�g�̐��|�l�̕���

        int robotEffectRate = 0;                                                                // ���{�b�g�̉e����
        removeRate = 0;                                                                         // ������

        // ���{�b�g�̉e�����̐ݒ�
        if (cleanAverage <= 0)
        {
            if (pollutionRank <= 0)
                robotEffectRate = 50;
            else if (pollutionRank == 1)
                robotEffectRate = 40;
            else if (pollutionRank == 2)
                robotEffectRate = 30;
            else if (pollutionRank == 3)
                robotEffectRate = 20;
            else if (pollutionRank == 4)
                robotEffectRate = 10;
            else
                robotEffectRate = 5;
        }
        else if (cleanAverage == 1)
        {
            if (pollutionRank <= 0)
                robotEffectRate = 60;
            else if (pollutionRank == 1)
                robotEffectRate = 50;
            else if (pollutionRank == 2)
                robotEffectRate = 40;
            else if (pollutionRank == 3)
                robotEffectRate = 30;
            else if (pollutionRank == 4)
                robotEffectRate = 20;
            else
                robotEffectRate = 10;
        }
        else if (cleanAverage == 2)
        {
            if (pollutionRank <= 0)
                robotEffectRate = 70;
            else if (pollutionRank == 1)
                robotEffectRate = 60;
            else if (pollutionRank == 2)
                robotEffectRate = 50;
            else if (pollutionRank == 3)
                robotEffectRate = 40;
            else if (pollutionRank == 4)
                robotEffectRate = 30;
            else
                robotEffectRate = 20;
        }
        else if (cleanAverage == 3)
        {
            if (pollutionRank <= 0)
                robotEffectRate = 80;
            else if (pollutionRank == 1)
                robotEffectRate = 70;
            else if (pollutionRank == 2)
                robotEffectRate = 60;
            else if (pollutionRank == 3)
                robotEffectRate = 50;
            else if (pollutionRank == 4)
                robotEffectRate = 40;
            else
                robotEffectRate = 30;
        }
        else if (cleanAverage == 4)
        {
            if (pollutionRank <= 0)
                robotEffectRate = 90;
            else if (pollutionRank == 1)
                robotEffectRate = 80;
            else if (pollutionRank == 2)
                robotEffectRate = 70;
            else if (pollutionRank == 3)
                robotEffectRate = 60;
            else if (pollutionRank == 4)
                robotEffectRate = 50;
            else
                robotEffectRate = 40;
        }
        else
        {
            if (pollutionRank <= 0)
                robotEffectRate = 100;
            else if (pollutionRank == 1)
                robotEffectRate = 100;
            else if (pollutionRank == 2)
                robotEffectRate = 90;
            else if (pollutionRank == 3)
                robotEffectRate = 80;
            else if (pollutionRank == 4)
                robotEffectRate = 70;
            else
                robotEffectRate = 60;
        }

        // ����Z�\�Ɠ��̖�肪��v�����Ƃ��̃{�[�i�X�e����
        if (robot1.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_SPECIALIZED_FOR_CLEANING && problem == PROBLEM_LIST.PROBLEM_TRASH || robot2.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_SPECIALIZED_FOR_CLEANING && problem == PROBLEM_LIST.PROBLEM_TRASH)
            robotEffectRate += 20;  // ���|�������S�~���
        
        if (robot1.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY && problem == PROBLEM_LIST.PROBLEM_PLASTIC || robot2.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY && problem == PROBLEM_LIST.PROBLEM_PLASTIC)
            robotEffectRate += 20;  // �v���X�`�b�N��p���v���X�`�b�N���
        
        if (robot1.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION && problem == PROBLEM_LIST.PROBLEM_FUELOIL || robot2.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION && problem == PROBLEM_LIST.PROBLEM_FUELOIL)
            robotEffectRate += 20;  // ��������d�����

        // ���{�b�g�̉e�������牘���������̐ݒ�
        if (robotEffectRate == 120)
            removeRate = 100;
        else if (robotEffectRate == 110)
            removeRate = 80;
        else if (robotEffectRate == 100)
            removeRate = 60;
        else if (robotEffectRate == 90)
            removeRate = 55;
        else if (robotEffectRate == 80)
            removeRate = 50;
        else if (robotEffectRate == 70)
            removeRate = 45;
        else if (robotEffectRate == 60)
            removeRate = 40;
        else if (robotEffectRate == 50)
            removeRate = 35;
        else if (robotEffectRate == 40)
            removeRate = 30;
        else if (robotEffectRate == 30)
            removeRate = 25;
        else if (robotEffectRate == 25)
            removeRate = 20;
        else if (robotEffectRate == 20)
            removeRate = 15;
        else if (robotEffectRate == 10)
            removeRate = 10;
        else
            removeRate = 5;

        return removeRate;
    }

    // ���������牘���x������������֐�
    public void RemovePollution(int removeRate)
    {
        pollutionLevel -= removeRate;
        IslandManager.Instance.CheckTotalPollutionLevel();
    }

    // �����J�n�֐�
    public void StartInvestigate(float time)
    {
        timer.GetComponent<Timer>().TimerStart(time, FinishInvestigate);
        state = STATE_ISLAND.STATE_INVESTIGATING;
        timer.SetActive(true);
    }

    // �����I���֐�
    private void FinishInvestigate()
    {
        timer.SetActive(false);
        state = STATE_ISLAND.STATE_INVESTIGATED;
        checkInvestigated = true;
    }

    // �����J�n�֐�
    public void StartClean(float time)
    {
        timer.GetComponent<Timer>().TimerStart(time, FinishClean);
        state = STATE_ISLAND.STATE_CLEANING;
        timer.SetActive(true);
    }

    // �����I���֐�
    private void FinishClean()
    {
        timer.SetActive(false);
        CalcRemoveRate();
        RemovePollution(removeRate);

        if (pollutionLevel <= 0)
            state = STATE_ISLAND.STATE_CLEANED;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        if (checkInvestigated)// ������
            pollutionLevelText.text = "�C�m�����x�F" + pollutionLevel.ToString() + "%";
        else// ������
            pollutionLevelText.text = "�C�m�����x�F---%";
    }
}
