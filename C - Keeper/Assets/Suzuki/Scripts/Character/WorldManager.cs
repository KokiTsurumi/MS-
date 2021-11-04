using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : SingletonMonoBehaviour<WorldManager>
{
    [SerializeField, Range(1, 5)] private int popularityRank = 1;   // �m���x�����N

    // �m���x�����N���グ��֐�
    public void IncreasePopularityRank()
    {
        popularityRank++;

        if (popularityRank >= 5)
            popularityRank = 5;

        // �f�o�b�O
        Debug.Log(popularityRank);
    }

    // ���݂̒m���x�����N��Ԃ��֐�
    public int GetPopularityRank()
    {
        return popularityRank;
    }

    // Start is called before the first frame update
    void Start()
    {
        // �f�o�b�O
        //Debug.Log(popularityRank);
    }

    // Update is called once per frame
    void Update()
    {
        // �f�o�b�O
        //if (Input.GetKeyDown(KeyCode.Space))
        //    IncreasePopularityRank();
    }
}
