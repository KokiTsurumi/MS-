using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : SingletonMonoBehaviour<RobotManager>
{
    // メンバ変数
    public List<GameObject> RobotList = new List<GameObject>();     // 保有しているロボットのリスト

    public GameObject RobotPrefab;
    public GameObject list;



    // 二機の清掃値の平均を返す関数
    public int GetCleanAverage(RobotData robot1, RobotData robot2)
    {
        float average = (robot1.clean + robot2.clean) / 2;
        return Mathf.CeilToInt(average);
    }

    // 二機の駆動時間の平均を返す関数
    public int GetBatteryAverage(RobotData robot1, RobotData robot2)
    {
        float average = (robot1.battery + robot2.battery) / 2;
        return Mathf.CeilToInt(average);
    }

    // 清掃にかかる時間(秒)を計算して渡す関数
    public float CalcCleanTime(RobotData robot1, RobotData robot2)
    {
        float sec = 0;

        if (robot1.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY || robot2.specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY) // どちらかがバッテリーお化け(仮)を持ってる場合
        {
            if (GetBatteryAverage(robot1, robot2) <= 0)       // E
                sec = 15;
            else if (GetBatteryAverage(robot1, robot2) == 1)  // D
                sec = 13;
            else if (GetBatteryAverage(robot1, robot2) == 2)  // C
                sec = 10;
            else if (GetBatteryAverage(robot1, robot2) == 3)  // B
                sec = 8;
            else if (GetBatteryAverage(robot1, robot2) == 4)  // A
                sec = 6;
            else                                              // S
                sec = 4;
        }
        else // タッグ機能無し
        {
            if (GetBatteryAverage(robot1, robot2) <= 0)       // E
                sec = 20;
            else if (GetBatteryAverage(robot1, robot2) == 1)  // D
                sec = 18;
            else if (GetBatteryAverage(robot1, robot2) == 2)  // C
                sec = 16;
            else if (GetBatteryAverage(robot1, robot2) == 3)  // B
                sec = 14;
            else if (GetBatteryAverage(robot1, robot2) == 4)  // A
                sec = 12;
            else                                              // S
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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GameObject obj = Instantiate(RobotPrefab);
            obj.transform.parent = list.transform;
            RobotList.Add(obj);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            RobotList.Clear();
        }
    }
}
