using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : SingletonMonoBehaviour<WorldManager>
{
    [SerializeField, Range(1, 5)] private int popularityRank = 1;   // 知名度ランク



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



    // Start is called before the first frame update
    void Start()
    {
        // デバッグ
        //Debug.Log(popularityRank);
    }

    // Update is called once per frame
    void Update()
    {
        // デバッグ
        //if (Input.GetKeyDown(KeyCode.Space))
        //    IncreasePopularityRank();
    }
}
