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

    // 島の状態(ステートマシン)
    public enum STATE_ISLAND
    {
        STATE_UNINVESTIGATED,   // 未調査
        STATE_INVESTIGATING,    // 調査中
        STATE_INVESTIGATED,     // 調査済
        STATE_CLEANING,         // 清掃中
        STATE_CLEANED,          // 清掃済
    }



    // メンバ変数
    [SerializeField, Range(0, 100)] private int pollutionLevel = 100;   // 汚染度(0%～100%)
    [SerializeField, Range(0, 5)] private int pollutionRank = 0;        // 汚染ランク(0～5:E～S)
    [SerializeField] protected bool checkInvestigated = false;          // 島の調査が完了しているかのフラグ
    [SerializeField] protected Text pollutionLevelText;                 // 島の汚染度を表示するためのText

    int removeRate;                                                     // 除去率

    public PROBLEM_LIST problem;                                        // 島が抱えてる問題
    public STATE_ISLAND state;                                          // 島の状態

    public GameObject timer;                                            // タイマーオブジェクト
    public GameObject icon;                                             // ビックリマークアイコンオブジェクト



    // 島の汚染度を取得する関数
    public int GetPollutionLevel()
    {
        return pollutionLevel;
    }

    // 島の除去率を計算する関数
    public int CalcRemoveRate()
    {
        RobotData robot1 = RobotManager.Instance.selectedRobot[0].GetComponent<RobotData>();    // ロボット１
        RobotData robot2 = RobotManager.Instance.selectedRobot[1].GetComponent<RobotData>();    // ロボット２

        int cleanAverage = RobotManager.Instance.GetCleanAverage(robot1, robot2);               // ロボットの清掃値の平均

        int robotEffectRate = 0;                                                                // ロボットの影響率
        removeRate = 0;                                                                         // 除去率

        // ロボットの影響率の設定
        if (cleanAverage <= 0)
        {
            if (pollutionRank <= 0)
                robotEffectRate = 50;
            else if (pollutionRank == 1)
                robotEffectRate = 40;
            else if (pollutionRank == 2)
                robotEffectRate = 30;
            else if (pollutionRank == 3)
                robotEffectRate = 20;
            else if (pollutionRank == 4)
                robotEffectRate = 10;
            else
                robotEffectRate = 5;
        }
        else if (cleanAverage == 1)
        {
            if (pollutionRank <= 0)
                robotEffectRate = 60;
            else if (pollutionRank == 1)
                robotEffectRate = 50;
            else if (pollutionRank == 2)
                robotEffectRate = 40;
            else if (pollutionRank == 3)
                robotEffectRate = 30;
            else if (pollutionRank == 4)
                robotEffectRate = 20;
            else
                robotEffectRate = 10;
        }
        else if (cleanAverage == 2)
        {
            if (pollutionRank <= 0)
                robotEffectRate = 70;
            else if (pollutionRank == 1)
                robotEffectRate = 60;
            else if (pollutionRank == 2)
                robotEffectRate = 50;
            else if (pollutionRank == 3)
                robotEffectRate = 40;
            else if (pollutionRank == 4)
                robotEffectRate = 30;
            else
                robotEffectRate = 20;
        }
        else if (cleanAverage == 3)
        {
            if (pollutionRank <= 0)
                robotEffectRate = 80;
            else if (pollutionRank == 1)
                robotEffectRate = 70;
            else if (pollutionRank == 2)
                robotEffectRate = 60;
            else if (pollutionRank == 3)
                robotEffectRate = 50;
            else if (pollutionRank == 4)
                robotEffectRate = 40;
            else
                robotEffectRate = 30;
        }
        else if (cleanAverage == 4)
        {
            if (pollutionRank <= 0)
                robotEffectRate = 90;
            else if (pollutionRank == 1)
                robotEffectRate = 80;
            else if (pollutionRank == 2)
                robotEffectRate = 70;
            else if (pollutionRank == 3)
                robotEffectRate = 60;
            else if (pollutionRank == 4)
                robotEffectRate = 50;
            else
                robotEffectRate = 40;
        }
        else
        {
            if (pollutionRank <= 0)
                robotEffectRate = 100;
            else if (pollutionRank == 1)
                robotEffectRate = 100;
            else if (pollutionRank == 2)
                robotEffectRate = 90;
            else if (pollutionRank == 3)
                robotEffectRate = 80;
            else if (pollutionRank == 4)
                robotEffectRate = 70;
            else
                robotEffectRate = 60;
        }

        // 特殊技能と島の問題が一致したときのボーナス影響率
        if (robot1.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_SPECIALIZED_FOR_CLEANING && problem == PROBLEM_LIST.PROBLEM_TRASH || robot2.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_SPECIALIZED_FOR_CLEANING && problem == PROBLEM_LIST.PROBLEM_TRASH)
            robotEffectRate += 20;  // 清掃特化＆ゴミ問題
        
        if (robot1.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY && problem == PROBLEM_LIST.PROBLEM_PLASTIC || robot2.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY && problem == PROBLEM_LIST.PROBLEM_PLASTIC)
            robotEffectRate += 20;  // プラスチック専用＆プラスチック問題
        
        if (robot1.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION && problem == PROBLEM_LIST.PROBLEM_FUELOIL || robot2.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION && problem == PROBLEM_LIST.PROBLEM_FUELOIL)
            robotEffectRate += 20;  // 油回収＆重油問題

        // ロボットの影響率から汚染除去率の設定
        if (robotEffectRate == 120)
            removeRate = 100;
        else if (robotEffectRate == 110)
            removeRate = 80;
        else if (robotEffectRate == 100)
            removeRate = 60;
        else if (robotEffectRate == 90)
            removeRate = 55;
        else if (robotEffectRate == 80)
            removeRate = 50;
        else if (robotEffectRate == 70)
            removeRate = 45;
        else if (robotEffectRate == 60)
            removeRate = 40;
        else if (robotEffectRate == 50)
            removeRate = 35;
        else if (robotEffectRate == 40)
            removeRate = 30;
        else if (robotEffectRate == 30)
            removeRate = 25;
        else if (robotEffectRate == 25)
            removeRate = 20;
        else if (robotEffectRate == 20)
            removeRate = 15;
        else if (robotEffectRate == 10)
            removeRate = 10;
        else
            removeRate = 5;

        return removeRate;
    }

    // 除去率から汚染度を減少させる関数
    public void RemovePollution(int removeRate)
    {
        pollutionLevel -= removeRate;
        IslandManager.Instance.CheckTotalPollutionLevel();
    }

    // 調査開始関数
    public void StartInvestigate(float time)
    {
        timer.GetComponent<Timer>().TimerStart(time, FinishInvestigate);
        state = STATE_ISLAND.STATE_INVESTIGATING;
        timer.SetActive(true);
    }

    // 調査終了関数
    private void FinishInvestigate()
    {
        timer.SetActive(false);
        state = STATE_ISLAND.STATE_INVESTIGATED;
        checkInvestigated = true;
    }

    // 調査開始関数
    public void StartClean(float time)
    {
        timer.GetComponent<Timer>().TimerStart(time, FinishClean);
        state = STATE_ISLAND.STATE_CLEANING;
        timer.SetActive(true);
    }

    // 調査終了関数
    private void FinishClean()
    {
        timer.SetActive(false);
        CalcRemoveRate();
        RemovePollution(removeRate);

        if (pollutionLevel <= 0)
            state = STATE_ISLAND.STATE_CLEANED;
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
