using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Island : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int pollutionLevel = 100;   // 汚染度(0%～100%)
    [SerializeField] private bool checkInvestigated = false;            // 島の調査が完了しているかのフラグ
    [SerializeField] private Text pollutionLevelText;                   // 島の汚染度を表示するためのText

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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkInvestigated)// 調査済
            pollutionLevelText.text = "海洋汚染度：" + pollutionLevel.ToString() + "%";
        else// 未調査
            pollutionLevelText.text = "海洋汚染度：---%";
    }
}
