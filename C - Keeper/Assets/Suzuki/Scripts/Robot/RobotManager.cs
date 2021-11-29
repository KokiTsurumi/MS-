using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : SingletonMonoBehaviour<RobotManager>
{
    // メンバ変数
    public List<GameObject> robotList = new List<GameObject>();     // 保有しているロボットのリスト
    public GameObject[] selectedRobot = new GameObject[2];          // 選択されたロボット

    public GameObject robotPrefab;
    public GameObject list;

    public Sprite robotSprite;                                      // ロボットの画像



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

    // ロボットの画像を設定する関数
    public void SetRobotSprite()
    {
        string directoryPath = "ロボ";    　// ロボット画像の入ったパスを指定
        Sprite[] spriteList;                // 取得したロボット画像を保持するリスト

        spriteList = Resources.LoadAll<Sprite>(directoryPath);   // ロボット画像を全て取得

        // キャラクター画像をランダムで決定
        int index = Random.Range(0, spriteList.Length);
        robotSprite = spriteList[index];
    }

    public GameObject GenerateRobot()
    {
        GameObject obj = Instantiate(robotPrefab);
        obj.transform.parent = list.transform;
        robotList.Add(obj);

        return obj;
    }



    // Start is called before the first frame update
    void Start()
    {
        GenerateRobot();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GenerateRobot();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            robotList.Clear();
        }
    }
}
