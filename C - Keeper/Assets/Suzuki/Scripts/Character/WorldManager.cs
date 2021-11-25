using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : SingletonMonoBehaviour<WorldManager>
{
    [SerializeField, Range(1, 5)] private int popularityRank = 1;   // 知名度ランク



    // 知名度ランクを上げる関数
    public void IncreasePopularityRank()
    {
        popularityRank++;

        if (popularityRank >= 5)
            popularityRank = 5;

        // デバッグ
        Debug.Log(popularityRank);
    }

    // 現在の知名度ランクを返す関数
    public int GetPopularityRank()
    {
        return popularityRank;
    }

    // タイマーセット関数
    //public void SetTimer(float sec, Vector3 position)
    //{
    //    IslandManager.Instance.currentIsland.GetComponent<IslandBase>().timer.GetComponent<Timer>().TimerStart(sec);            // 時間
    //    IslandManager.Instance.currentIsland.GetComponent<IslandBase>().timer.GetComponent<Transform>().position = position;    // 位置
    //}



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
