using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : SingletonMonoBehaviour<RobotManager>
{
    // �����o�ϐ�
    public List<GameObject> robotList = new List<GameObject>();     // �ۗL���Ă��郍�{�b�g�̃��X�g
    public GameObject selectedRobot;                                // �I�����ꂽ���{�b�g

    public GameObject robotPrefab;
    public GameObject list;

    public Sprite robotSprite;                                      // ���{�b�g�̉摜



    /// <summary>
    /// ���|�ɂ����鎞�Ԃ��v�Z���ēn���֐�
    /// </summary>
    /// <returns>�b</returns>
    public float CalcCleanTime()
    {
        float sec = 0;

        if (selectedRobot.GetComponent<RobotData>().specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY) // �o�b�e���[������(��)�������Ă�ꍇ
        {
            if (selectedRobot.GetComponent<RobotData>().battery <= 0)       // E
                sec = 15;
            else if (selectedRobot.GetComponent<RobotData>().battery == 1)  // D
                sec = 13;
            else if (selectedRobot.GetComponent<RobotData>().battery == 2)  // C
                sec = 10;
            else if (selectedRobot.GetComponent<RobotData>().battery == 3)  // B
                sec = 8;
            else if (selectedRobot.GetComponent<RobotData>().battery == 4)  // A
                sec = 6;
            else                                                            // S
                sec = 4;
        }
        else // �^�b�O�@�\����
        {
            if (selectedRobot.GetComponent<RobotData>().battery <= 0)       // E
                sec = 20;
            else if (selectedRobot.GetComponent<RobotData>().battery == 1)  // D
                sec = 18;
            else if (selectedRobot.GetComponent<RobotData>().battery == 2)  // C
                sec = 16;
            else if (selectedRobot.GetComponent<RobotData>().battery == 3)  // B
                sec = 14;
            else if (selectedRobot.GetComponent<RobotData>().battery == 4)  // A
                sec = 12;
            else                                                            // S
                sec = 10;
        }

        return sec;
    }

    /// <summary>
    /// �����N(���l)�������N(�A���t�@�x�b�g)�ɕϊ�����֐�
    /// </summary>
    /// <param name="Param">���{�b�g�̃p�����[�^������</param>
    /// <returns>�����N�ɑΉ������A���t�@�x�b�g</returns>
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

    /// <summary>
    /// ���{�b�g�̉摜��ݒ肷��֐�
    /// </summary>
    public void SetRobotSprite()
    {
        string directoryPath = "���{";    �@// ���{�b�g�摜�̓������p�X���w��
        Sprite[] spriteList;                // �擾�������{�b�g�摜��ێ����郊�X�g

        spriteList = Resources.LoadAll<Sprite>(directoryPath);   // ���{�b�g�摜��S�Ď擾

        // ���{�b�g�摜�������_���Ō���
        int index = Random.Range(0, spriteList.Length);
        robotSprite = spriteList[index];
    }

    /// <summary>
    /// ���{�b�g�𐶐�����֐�
    /// </summary>
    /// <returns>���������I�u�W�F�N�g</returns>
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
        
    }
}
