using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IslandBase : MonoBehaviour
{
    // 島が抱えてる問題
    public enum PROBLEM_LIST
    {
        PROBLEM_TRASH,      // ゴミ問題
        PROBLEM_PLASTIC,    // プラスチック問題
        PROBLEM_FUELOIL,    // 重油問題

        PROBLEM_MAX,        // 問題の種類の最大数
    }



    // メンバ変数
    [SerializeField, Range(0, 100)] private int pollutionLevel = 100;   // 汚染度(0%～100%)
    [SerializeField] protected bool checkInvestigated = false;          // 島の調査が完了しているかのフラグ
    [SerializeField] protected Text pollutionLevelText;                 // 島の汚染度を表示するためのText

    public PROBLEM_LIST problem;                                        // 島が抱えてる問題

    public GameObject timer;                                            // タイマーオブジェクト

    

    // 島の汚染度を取得する関数
    public int GetPollutionLevel()
    {
        return pollutionLevel;
    }

    // 島が調査済かどうかを返す関数
    public bool GetCheckInvastigate()
    {
        return checkInvestigated;
    }

    // 調査完了フラグをtrueにする関数
    public void CompleteInvastigate()
    {
        checkInvestigated = true;
    }



    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        if (checkInvestigated)// 調査済
            pollutionLevelText.text = "海洋汚染度：" + pollutionLevel.ToString() + "%";
        else// 未調査
            pollutionLevelText.text = "海洋汚染度：---%";
    }
}
