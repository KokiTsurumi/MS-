using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IslandBase : MonoBehaviour
{
    /// <summary>
    /// ���������Ă���̎��
    /// </summary>
    public enum PROBLEM_LIST
    {
        PROBLEM_TRASH,      // �S�~���
        PROBLEM_PLASTIC,    // �v���X�`�b�N���
        PROBLEM_FUELOIL,    // �d�����

        PROBLEM_MAX,        // ���̎�ނ̍ő吔
    }

    /// <summary>
    /// ���̏��(�X�e�[�g�}�V��)
    /// </summary>
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
    [SerializeField] protected Text InvestigateCompleteText;            // �����I�����ɕ\��������Text

    int removeRate;                                                     // ������

    public PROBLEM_LIST problem;                                        // ���������Ă���
    public STATE_ISLAND state;                                          // ���̏��

    public GameObject timer;                                            // �^�C�}�[�I�u�W�F�N�g
    public GameObject icon;                                             // �r�b�N���}�[�N�A�C�R���I�u�W�F�N�g



    /// <summary>
    /// ���̉����x���擾����֐�
    /// </summary>
    /// <returns>���̉����x</returns>
    public int GetPollutionLevel()
    {
        return pollutionLevel;
    }

    /// <summary>
    /// ���̉����x���v�Z����֐�
    /// </summary>
    /// <param name="isTutorial">�`���[�g���A���p�Ȃ�true</param>
    /// <returns>������</returns>
    public int CalcRemoveRate(bool isTutorial)
    {
        int robotEffectRate = 0;                                                                // ���{�b�g�̉e����
        removeRate = 0;                                                                         // ������

        // ���{�b�g�̉e�����̐ݒ�
        if (RobotManager.Instance.selectedRobot.GetComponent<RobotData>().clean <= 0)
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
        else if (RobotManager.Instance.selectedRobot.GetComponent<RobotData>().clean == 1)
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
        else if (RobotManager.Instance.selectedRobot.GetComponent<RobotData>().clean == 2)
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
        else if (RobotManager.Instance.selectedRobot.GetComponent<RobotData>().clean == 3)
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
        else if (RobotManager.Instance.selectedRobot.GetComponent<RobotData>().clean == 4)
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
        if (RobotManager.Instance.selectedRobot.GetComponent<RobotData>().specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_SPECIALIZED_FOR_CLEANING && problem == PROBLEM_LIST.PROBLEM_TRASH)
            robotEffectRate += 20;  // ���|�������S�~���
        
        if (RobotManager.Instance.selectedRobot.GetComponent<RobotData>().specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY && problem == PROBLEM_LIST.PROBLEM_PLASTIC)
            robotEffectRate += 20;  // �v���X�`�b�N��p���v���X�`�b�N���
        
        if (RobotManager.Instance.selectedRobot.GetComponent<RobotData>().specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION && problem == PROBLEM_LIST.PROBLEM_FUELOIL)
            robotEffectRate += 20;  // ��������d�����

        if (RobotManager.Instance.selectedRobot.GetComponent<RobotData>().specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_ALMIGHTY)
            robotEffectRate += 20;  // �I�[���}�C�e�B�͖�������+20

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

        // �`���[�g���A���p
        if (isTutorial)
            removeRate = 100;

        return removeRate;
    }

    /// <summary>
    /// ���������牘���x������������֐�
    /// </summary>
    /// <param name="removeRate">������</param>
    public void RemovePollution(int removeRate)
    {
        pollutionLevel -= removeRate;

        if (pollutionLevel < 0)
            pollutionLevel = 0;

        IslandManager.Instance.CheckTotalPollutionLevel();
    }

    /// <summary>
    /// �����J�n�֐�
    /// </summary>
    /// <param name="time">����</param>
    /// <param name="callback">�R�[���o�b�N�֐�</param>
    public void StartInvestigate(float time, Timer.CallBack callback)
    {
        timer.GetComponent<Timer>().TimerStart(time, FinishInvestigate, callback);
        state = STATE_ISLAND.STATE_INVESTIGATING;
        timer.SetActive(true);
    }

    /// <summary>
    /// �����I���֐�
    /// </summary>
    private void FinishInvestigate()
    {
        //timer.SetActive(false);
        state = STATE_ISLAND.STATE_INVESTIGATED;
        checkInvestigated = true;
        InvestigateCompleteText.gameObject.SetActive(true);
        icon.SetActive(true);
    }

    /// <summary>
    /// ���|�J�n�֐�
    /// </summary>
    /// <param name="time">����</param>
    /// <param name="callback">�R�[���o�b�N�֐�</param>
    public void StartClean(float time, Timer.CallBack callback)
    {
        timer.GetComponent<Timer>().TimerStart(time, FinishClean, callback);
        state = STATE_ISLAND.STATE_CLEANING;
        timer.SetActive(true);
        //pollutionLevelText.gameObject.SetActive(false);
        InvestigateCompleteText.gameObject.SetActive(false);
    }

    /// <summary>
    /// ���|�I���֐�
    /// </summary>
    private void FinishClean()
    {
        //timer.SetActive(false);
        CalcRemoveRate(false);
        RemovePollution(removeRate);
        //pollutionLevelText.gameObject.SetActive(true);

        if (pollutionLevel <= 0)
            state = STATE_ISLAND.STATE_CLEANED;
    }

    /// <summary>
    /// ���Y�J�n�֐�
    /// </summary>
    /// <param name="time">����</param>
    /// <param name="callback">�R�[���o�b�N�֐�</param>
    public void StartProduction(float time, Timer.CallBack callback)
    {
        timer.GetComponent<Timer>().TimerStart(time, callback);
        timer.SetActive(true);
        pollutionLevelText.gameObject.SetActive(false);
    }

    /// <summary>
    /// ���Y�I���֐�
    /// </summary>
    private void FinishProduction()
    {
        //timer.SetActive(false);
        CalcRemoveRate(false);
        RemovePollution(removeRate);
        pollutionLevelText.gameObject.SetActive(true);
    }


    // Start is called before the first frame update
    protected void Start()
    {
        pollutionLevelText.gameObject.SetActive(false);
        InvestigateCompleteText.gameObject.SetActive(false);
        icon.SetActive(false);
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
}
