using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : SingletonMonoBehaviour<IslandManager>
{
    // �����o�ϐ�
    public List<GameObject> islandList = new List<GameObject>();    // ���̃��X�g

    [SerializeField, Range(0, 100)]
    public float totalPollutionLevel;                               // �S�̂̉����x

    private GameObject currentIsland;                               // ���ݑI�𒆂̓�



    /// <summary>
    /// �S�̂̉����x���v�Z����֐�
    /// </summary>
    public void CheckTotalPollutionLevel()
    {
        float tmp = 0;

        foreach(GameObject island in islandList)
        {
            tmp += island.GetComponent<IslandBase>().GetPollutionLevel();
        }

        totalPollutionLevel = tmp / islandList.Count;
    }

    /// <summary>
    /// ���݂̓���Ԃ��֐�
    /// </summary>
    /// <returns>���ݑI��ł��铇</returns>
    public GameObject GetCurrentIsland()
    {
        return currentIsland;
    }

    /// <summary>
    /// ���݂̓����Z�b�g����֐�
    /// </summary>
    /// <param name="island">�I�т�����</param>
    public void SetCurrentIsland(GameObject island)
    {
        currentIsland = island;
    }

    /// <summary>
    /// �����N(���l)�������N(�A���t�@�x�b�g)�ɕϊ�����֐�
    /// </summary>
    /// <param name="Param">���̉����x�����N</param>
    /// <returns>�����N�ɉ������A���t�@�x�b�g</returns>
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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
