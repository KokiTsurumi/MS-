using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : SingletonMonoBehaviour<RobotManager>
{
    // メンバ変数
    public List<GameObject> robotList = new List<GameObject>();     // 保有しているロボットのリスト
    public GameObject selectedRobot;                                // 選択されたロボット

    public GameObject robotPrefab;
    public GameObject list;

    public Sprite robotSprite;                                      // ロボットの画像



    /// <summary>
    /// 清掃にかかる時間を計算して渡す関数
    /// </summary>
    /// <returns>秒</returns>
    public float CalcCleanTime()
    {
        float sec = 0;

        if (selectedRobot.GetComponent<RobotData>().specialSkill == RobotBase.SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY) // バッテリーお化け(仮)を持ってる場合
        {
            if (selectedRobot.GetComponent<RobotData>().battery <= 0)       // E
                sec = 15;
            else if (selectedRobot.GetComponent<RobotData>().battery == 1)  // D
                sec = 13;
            else if (selectedRobot.GetComponent<RobotData>().battery == 2)  // C
                sec = 10;
            else if (selectedRobot.GetComponent<RobotData>().battery == 3)  // B
                sec = 8;
            else if (selectedRobot.GetComponent<RobotData>().battery == 4)  // A
                sec = 6;
            else                                                            // S
                sec = 4;
        }
        else // タッグ機能無し
        {
            if (selectedRobot.GetComponent<RobotData>().battery <= 0)       // E
                sec = 20;
            else if (selectedRobot.GetComponent<RobotData>().battery == 1)  // D
                sec = 18;
            else if (selectedRobot.GetComponent<RobotData>().battery == 2)  // C
                sec = 16;
            else if (selectedRobot.GetComponent<RobotData>().battery == 3)  // B
                sec = 14;
            else if (selectedRobot.GetComponent<RobotData>().battery == 4)  // A
                sec = 12;
            else                                                            // S
                sec = 10;
        }

        return sec;
    }

    /// <summary>
    /// ランク(数値)をランク(アルファベット)に変換する関数
    /// </summary>
    /// <param name="Param">ロボットのパラメータを入れる</param>
    /// <returns>ランクに対応したアルファベット</returns>
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

    /// <summary>
    /// ロボットの画像を設定する関数
    /// </summary>
    public void SetRobotSprite()
    {
        string directoryPath = "ロボ";    　// ロボット画像の入ったパスを指定
        Sprite[] spriteList;                // 取得したロボット画像を保持するリスト

        spriteList = Resources.LoadAll<Sprite>(directoryPath);   // ロボット画像を全て取得

        // ロボット画像をランダムで決定
        int index = Random.Range(0, spriteList.Length);
        robotSprite = spriteList[index];
    }

    /// <summary>
    /// ロボットを生成する関数
    /// </summary>
    /// <returns>生成したオブジェクト</returns>
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
        
    }
}
