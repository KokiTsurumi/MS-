using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : SingletonMonoBehaviour<WorldManager>
{
    [SerializeField, Range(1, 5)] private int popularityRank = 1;   // 知名度ランク
    [SerializeField] private float roopTime;                        // ゲーム内1か月の時間
    [SerializeField] private float currentTime;                     // 現在の時間
    [SerializeField] private int month = 0;                         // ゲーム内経過月



    /// <summary>
    /// 知名度ランクを上げる関数
    /// </summary>
    public void IncreasePopularityRank()
    {
        popularityRank++;

        if (popularityRank >= 5)
            popularityRank = 5;

        // デバッグ
        Debug.Log(popularityRank);
    }

    /// <summary>
    /// 現在の知名度ランクを返す関数
    /// </summary>
    /// <returns>知名度ランク</returns>
    public int GetPopularityRank()
    {
        return popularityRank;
    }

    /// <summary>
    /// ゲームクリア時の評価を返す関数
    /// </summary>
    /// <returns>ランク(アルファベット)</returns>
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
