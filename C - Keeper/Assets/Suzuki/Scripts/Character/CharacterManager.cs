using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // メンバ変数
    public List<GameObject> CharacterList = new List<GameObject>();     // 保有している人材のリスト
    public GameObject[] SelectedCharacter = new GameObject[2];          // 選択されたキャラクター


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
    public int CalcProductionTime(CharacterData chara1, CharacterData chara2)
    {
        int sec = 0;

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
    public int CalcInvestigationTime(CharacterData chara1, CharacterData chara2)
    {
        int sec = 0;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
