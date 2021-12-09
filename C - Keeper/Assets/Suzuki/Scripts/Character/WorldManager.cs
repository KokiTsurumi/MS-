using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : SingletonMonoBehaviour<WorldManager>
{
    [SerializeField, Range(1, 5)] private int popularityRank = 1;   // �m���x�����N



    /// <summary>
    /// �m���x�����N���グ��֐�
    /// </summary>
    public void IncreasePopularityRank()
    {
        popularityRank++;

        if (popularityRank >= 5)
            popularityRank = 5;

        // �f�o�b�O
        Debug.Log(popularityRank);
    }

    /// <summary>
    /// ���݂̒m���x�����N��Ԃ��֐�
    /// </summary>
    /// <returns>�m���x�����N</returns>
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
