using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : SingletonMonoBehaviour<IslandManager>
{
    // メンバ変数
    public List<GameObject> islandList = new List<GameObject>();    // 島のリスト

    [SerializeField, Range(0, 100)]
    public float totalPollutionLevel;                               // 全体の汚染度

    public GameObject currentIsland;                                // 現在選択中の島



    // 全体の汚染度を計算する関数
    public void CheckTotalPollutionLevel()
    {
        float tmp = 0;

        foreach(GameObject island in islandList)
        {
            tmp += island.GetComponent<IslandBase>().GetPollutionLevel();
        }

        totalPollutionLevel = tmp / islandList.Count;
    }

    // 現在の島を返す関数
    public GameObject GetCurrentIsland()
    {
        return currentIsland;
    }

    // 現在の島をセットする関数
    public void SetCurrentIsland(GameObject island)
    {
        currentIsland = island;
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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
