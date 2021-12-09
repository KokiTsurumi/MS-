using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : SingletonMonoBehaviour<WorldManager>
{
    [SerializeField, Range(1, 5)] private int popularityRank = 1;   // �m���x�����N
    [SerializeField] private float roopTime;                        // �Q�[����1�����̎���
    [SerializeField] private float currentTime;                     // ���݂̎���
    [SerializeField] private int month = 0;                         // �Q�[�����o�ߌ�



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

    /// <summary>
    /// �Q�[���N���A���̕]����Ԃ��֐�
    /// </summary>
    /// <returns>�����N(�A���t�@�x�b�g)</returns>
    public string EvaluateClearTime()
    {
        if (month < 10)
            return "S";
        else if (month >= 10 && month < 12)
            return "A";
        else if (month >= 12 && month < 14)
            return "B";
        else if (month >= 14 && month < 16)
            return "C";
        else if (month >= 16 && month < 18)
            return "D";
        else
            return "E";
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= roopTime)
        {
            month++;
            currentTime = 0;
        }
    }
}
