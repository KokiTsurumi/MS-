using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : SingletonMonoBehaviour<CharacterManager>
{
    // メンバ変数
    public List<GameObject> characterList = new List<GameObject>();     // 保有している人材のリスト
    public GameObject[] selectedCharacter = new GameObject[2];          // 選択されたキャラクター

    public GameObject characterPrefab;
    public GameObject list;



    // 二人の研究値の平均を返す関数
    public int GetReserchAverage(CharacterData chara1, CharacterData chara2)
    {
        float average = (chara1.research + chara2.research) / 2;
        return Mathf.CeilToInt(average);
    }

    // 二人の生産値の平均を返す関数
    public int GetProductionAverage(CharacterData chara1, CharacterData chara2)
    {
        float average = (chara1.production + chara2.production) / 2;
        return Mathf.CeilToInt(average);
    }

    // 二人の管理値の平均を返す関数
    public int GetManagementAverage(CharacterData chara1, CharacterData chara2)
    {
        float average = (chara1.management + chara2.management) / 2;
        return Mathf.CeilToInt(average);
    }

    // 二人の調査値の平均を返す関数
    public int GetInvestigationAverage(CharacterData chara1, CharacterData chara2)
    {
        float average = (chara1.investigation + chara2.investigation) / 2;
        return Mathf.CeilToInt(average);
    }

    // 生産にかかる時間(秒)を計算して渡す関数
    public float CalcProductionTime(CharacterData chara1, CharacterData chara2)
    {
        float sec = 0;

        if (chara1.tag == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH && chara2.tag == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH) // タッグ機能あり
        {
            if (GetProductionAverage(chara1, chara2) <= 0)       // E
                sec = 15;
            else if (GetProductionAverage(chara1, chara2) == 1)  // D
                sec = 13;
            else if (GetProductionAverage(chara1, chara2) == 2)  // C
                sec = 10;
            else if (GetProductionAverage(chara1, chara2) == 3)  // B
                sec = 8;
            else if (GetProductionAverage(chara1, chara2) == 4)  // A
                sec = 6;
            else                                                 // S
                sec = 4;
        }
        else // タッグ機能無し
        {
            if (GetProductionAverage(chara1, chara2) <= 0)       // E
                sec = 20;
            else if (GetProductionAverage(chara1, chara2) == 1)  // D
                sec = 18;
            else if (GetProductionAverage(chara1, chara2) == 2)  // C
                sec = 16;
            else if (GetProductionAverage(chara1, chara2) == 3)  // B
                sec = 14;
            else if (GetProductionAverage(chara1, chara2) == 4)  // A
                sec = 12;
            else                                                 // S
                sec = 10;
        }

        return sec;
    }

    // 調査にかかる時間(秒)を計算して渡す関数
    public float CalcInvestigationTime(CharacterData chara1, CharacterData chara2)
    {
        float sec = 0;

        if (chara1.tag == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH && chara2.tag == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH) // タッグ機能あり
        {
            if (GetInvestigationAverage(chara1, chara2) <= 0)       // E
                sec = 15;
            else if (GetInvestigationAverage(chara1, chara2) == 1)  // D
                sec = 13;
            else if (GetInvestigationAverage(chara1, chara2) == 2)  // C
                sec = 10;
            else if (GetInvestigationAverage(chara1, chara2) == 3)  // B
                sec = 8;
            else if (GetInvestigationAverage(chara1, chara2) == 4)  // A
                sec = 6;
            else                                                    // S
                sec = 4;
        }
        else // タッグ機能無し
        {
            if (GetInvestigationAverage(chara1, chara2) <= 0)       // E
                sec = 20;
            else if (GetInvestigationAverage(chara1, chara2) == 1)  // D
                sec = 18;
            else if (GetInvestigationAverage(chara1, chara2) == 2)  // C
                sec = 16;
            else if (GetInvestigationAverage(chara1, chara2) == 3)  // B
                sec = 14;
            else if (GetInvestigationAverage(chara1, chara2) == 4)  // A
                sec = 12;
            else                                                    // S
                sec = 10;
        }

        return sec;
    }

    // ランク(数値)をランク(アルファベット)に変換する関数
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



    private void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject obj = Instantiate(characterPrefab);
            obj.transform.parent = list.transform;
            characterList.Add(obj);
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
