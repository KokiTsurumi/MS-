using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : SingletonMonoBehaviour<RobotManager>
{
    // �����o�ϐ�
    public List<GameObject> robotList = new List<GameObject>();     // �ۗL���Ă��郍�{�b�g�̃��X�g
    public GameObject[] selectedRobot = new GameObject[2];          // �I�����ꂽ���{�b�g

    public GameObject robotPrefab;
    public GameObject list;

    public Sprite robotSprite;                                      // ���{�b�g�̉摜



    // ��@�̐��|�l�̕��ς�Ԃ��֐�
    public int GetCleanAverage(RobotData robot1, RobotData robot2)
    {
        float average = (robot1.clean + robot2.clean) / 2;
        return Mathf.CeilToInt(average);
    }

    // ��@�̋쓮���Ԃ̕��ς�Ԃ��֐�
    public int GetBatteryAverage(RobotData robot1, RobotData robot2)
    {
        float average = (robot1.battery + robot2.battery) / 2;
        return Mathf.CeilToInt(average);
    }

    // ���|�ɂ����鎞��(�b)���v�Z���ēn���֐�
    public float CalcCleanTime(RobotData robot1, RobotData robot2)
    {
        float sec = 0;

        if (robot1.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY || robot2.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY) // �ǂ��炩���o�b�e���[������(��)�������Ă�ꍇ
        {
            if (GetBatteryAverage(robot1, robot2) <= 0)       // E
                sec = 15;
            else if (GetBatteryAverage(robot1, robot2) == 1)  // D
                sec = 13;
            else if (GetBatteryAverage(robot1, robot2) == 2)  // C
                sec = 10;
            else if (GetBatteryAverage(robot1, robot2) == 3)  // B
                sec = 8;
            else if (GetBatteryAverage(robot1, robot2) == 4)  // A
                sec = 6;
            else                                              // S
                sec = 4;
        }
        else // �^�b�O�@�\����
        {
            if (GetBatteryAverage(robot1, robot2) <= 0)       // E
                sec = 20;
            else if (GetBatteryAverage(robot1, robot2) == 1)  // D
                sec = 18;
            else if (GetBatteryAverage(robot1, robot2) == 2)  // C
                sec = 16;
            else if (GetBatteryAverage(robot1, robot2) == 3)  // B
                sec = 14;
            else if (GetBatteryAverage(robot1, robot2) == 4)  // A
                sec = 12;
            else                                              // S
                sec = 10;
        }

        return sec;
    }

    // �����N(���l)�������N(�A���t�@�x�b�g)�ɕϊ�����֐�
    public string RankTransfer(int Param)
    {
        string tmp;

        if (Param <= 0)
            tmp = "E";
        else if (Param == 1)
            tmp = "D";
        else if (Param == 2)
            tmp = "C";
        else if (Param == 3)
            tmp = "B";
        else if (Param == 4)
            tmp = "A";
        else
            tmp = "S";

        return tmp;
    }

    // ���{�b�g�̉摜��ݒ肷��֐�
    public void SetRobotSprite()
    {
        string directoryPath = "���{";    �@// ���{�b�g�摜�̓������p�X���w��
        Sprite[] spriteList;                // �擾�������{�b�g�摜��ێ����郊�X�g

        spriteList = Resources.LoadAll<Sprite>(directoryPath);   // ���{�b�g�摜��S�Ď擾

        // �L�����N�^�[�摜�������_���Ō���
        int index = Random.Range(0, spriteList.Length);
        robotSprite = spriteList[index];
    }

    public GameObject GenerateRobot()
    {
        GameObject obj = Instantiate(robotPrefab);
        obj.transform.parent = list.transform;
        robotList.Add(obj);

        return obj;
    }



    // Start is called before the first frame update
    void Start()
    {
        GenerateRobot();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GenerateRobot();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            robotList.Clear();
        }
    }
}
