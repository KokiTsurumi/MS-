using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : SingletonMonoBehaviour<CharacterManager>
{
    // メンバ変数
    public List<GameObject> characterList = new List<GameObject>();     // 保有している人材のリスト
    public List<GameObject> candidateList = new List<GameObject>();     // 人材を雇う時の候補リスト
    public GameObject[] selectedCharacter = new GameObject[2];          // 選択されたキャラクター

    public GameObject characterPrefab;
    public GameObject list;



    /// <summary>
    /// 二人の研究値の平均を返す関数
    /// </summary>
    /// <returns>研究値の平均</returns>
    public int GetReserchAverage()
    {
        float average = (selectedCharacter[0].GetComponent<CharacterData>().research + selectedCharacter[1].GetComponent<CharacterData>().research) / 2;
        return Mathf.CeilToInt(average);
    }

    /// <summary>
    /// 二人の生産値の平均を返す関数
    /// </summary>
    /// <returns>生産値の平均</returns>
    public int GetProductionAverage()
    {
        float average = (selectedCharacter[0].GetComponent<CharacterData>().production + selectedCharacter[1].GetComponent<CharacterData>().production) / 2;
        return Mathf.CeilToInt(average);
    }

    /// <summary>
    /// 二人の管理値の平均を返す関数
    /// </summary>
    /// <returns>管理値の平均</returns>
    public int GetManagementAverage()
    {
        float average = (selectedCharacter[0].GetComponent<CharacterData>().management + selectedCharacter[1].GetComponent<CharacterData>().management) / 2;
        return Mathf.CeilToInt(average);
    }

    /// <summary>
    /// 二人の調査値の平均を返す関数
    /// </summary>
    /// <returns>調査値の平均</returns>
    public int GetInvestigationAverage()
    {
        float average = (selectedCharacter[0].GetComponent<CharacterData>().investigation + selectedCharacter[1].GetComponent<CharacterData>().investigation) / 2;
        return Mathf.CeilToInt(average);
    }

    /// <summary>
    /// 生産にかかる時間(秒)を計算して渡す関数
    /// </summary>
    /// <returns>秒</returns>
    public float CalcProductionTime()
    {
        float sec = 0;

        if (selectedCharacter[0].GetComponent<CharacterData>().tag == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH && selectedCharacter[1].GetComponent<CharacterData>().tag == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH) // タッグ機能あり
        {
            if (GetProductionAverage() <= 0)       // E
                sec = 15;
            else if (GetProductionAverage() == 1)  // D
                sec = 13;
            else if (GetProductionAverage() == 2)  // C
                sec = 10;
            else if (GetProductionAverage() == 3)  // B
                sec = 8;
            else if (GetProductionAverage() == 4)  // A
                sec = 6;
            else                                   // S
                sec = 4;
        }
        else // タッグ機能無し
        {
            if (GetProductionAverage() <= 0)       // E
                sec = 20;
            else if (GetProductionAverage() == 1)  // D
                sec = 18;
            else if (GetProductionAverage() == 2)  // C
                sec = 16;
            else if (GetProductionAverage() == 3)  // B
                sec = 14;
            else if (GetProductionAverage() == 4)  // A
                sec = 12;
            else                                   // S
                sec = 10;
        }

        return sec;
    }

    /// <summary>
    /// 調査にかかる時間(秒)を計算して渡す関数
    /// </summary>
    /// <returns>秒</returns>
    public float CalcInvestigationTime()
    {
        float sec = 0;

        if (selectedCharacter[0].GetComponent<CharacterData>().tag == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH && selectedCharacter[1].GetComponent<CharacterData>().tag == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH) // タッグ機能あり
        {
            if (GetInvestigationAverage() <= 0)       // E
                sec = 15;
            else if (GetInvestigationAverage() == 1)  // D
                sec = 13;
            else if (GetInvestigationAverage() == 2)  // C
                sec = 10;
            else if (GetInvestigationAverage() == 3)  // B
                sec = 8;
            else if (GetInvestigationAverage() == 4)  // A
                sec = 6;
            else                                      // S
                sec = 4;
        }
        else // タッグ機能無し
        {
            if (GetInvestigationAverage() <= 0)       // E
                sec = 20;
            else if (GetInvestigationAverage() == 1)  // D
                sec = 18;
            else if (GetInvestigationAverage() == 2)  // C
                sec = 16;
            else if (GetInvestigationAverage() == 3)  // B
                sec = 14;
            else if (GetInvestigationAverage() == 4)  // A
                sec = 12;
            else                                      // S
                sec = 10;
        }

        return sec;
    }

    /// <summary>
    /// ランク(数値)をランク(アルファベット)に変換する関数
    /// </summary>
    /// <param name="Param">キャラクターのパラメータ</param>
    /// <returns>ランクに応じたアルファベット</returns>
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
    /// 候補リストに5人生成する関数
    /// </summary>
    public void CreateCandidateCharacter()
    {
        for(int i = 0; i < 5; i++)
        {
            candidateList.Add(Instantiate(characterPrefab));
        }
    }

    /// <summary>
    /// 候補リストから2人選ぶ関数
    /// </summary>
    /// <param name="character1">1人目</param>
    /// <param name="character2">2人目</param>
    public void HireCharacter(GameObject character1, GameObject character2)
    {
        candidateList.Remove(character1);
        candidateList.Remove(character2);

        character1.transform.parent = list.transform;
        character2.transform.parent = list.transform;

        characterList.Add(character1);  // 1人目
        characterList.Add(character2);  // 2人目

        foreach(GameObject chara in candidateList)
        {
            Destroy(chara);
        }

        candidateList.Clear();          // 選ばれなかった人材を削除
    }

    /// <summary>
    /// キャラクター生成関数
    /// </summary>
    public void GenerateCharacter()
    {
        GameObject obj = Instantiate(characterPrefab);
        obj.transform.parent = list.transform;
        characterList.Add(obj);
    }

    /// <summary>
    /// 人材消費関数
    /// </summary>
    public void UseCharacter()
    {
        characterList.Remove(selectedCharacter[0]);
        characterList.Remove(selectedCharacter[1]);

        Destroy(selectedCharacter[0]);
        Destroy(selectedCharacter[1]);
    }



    private void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            GenerateCharacter();
        }

        selectedCharacter[0] = characterList[0];
        selectedCharacter[1] = characterList[1];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            selectedCharacter[0] = characterList[0];
            selectedCharacter[1] = characterList[1];
        }
    }
}
