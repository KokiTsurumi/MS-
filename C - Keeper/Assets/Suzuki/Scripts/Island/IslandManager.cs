using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : SingletonMonoBehaviour<IslandManager>
{
    // �����o�ϐ�
    public List<GameObject> islandList = new List<GameObject>();    // ���̃��X�g

    [SerializeField, Range(0, 100)]
    public float totalPollutionLevel;                               // �S�̂̉����x

    public GameObject currentIsland;                                // ���ݑI�𒆂̓�



    // �S�̂̉����x���v�Z����֐�
    public void CheckTotalPollutionLevel()
    {
        float tmp = 0;

        foreach(GameObject island in islandList)
        {
            tmp += island.GetComponent<IslandBase>().GetPollutionLevel();
        }

        totalPollutionLevel = tmp / islandList.Count;
    }

    // ���݂̓���Ԃ��֐�
    public GameObject GetCurrentIsland()
    {
        return currentIsland;
    }

    // ���݂̓����Z�b�g����֐�
    public void SetCurrentIsland(GameObject island)
    {
        currentIsland = island;
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
