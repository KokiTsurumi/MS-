using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ロボット選択　UI　インターフェース　クラス
/// </summary>
public class ActionRobotInterface : RobotData
{
    [SerializeField] Text nameText, cRank, bRank, skillText, skillName;
    [SerializeField] Image robotImage;

    CleaningCanvas canvas;

    //string robotName;
    //int clean;
    //int battery;
    //RobotBase.SPECIALSKILL_LIST skill;

    public GameObject originalGameObject;
    public bool isSelected { get; set; } = false;

    //public string GetName => robotName;
    //public int GetClean => clean;
    //public int GetBattery => battery;

    //public RobotBase.SPECIALSKILL_LIST GetSkill => skill;


    //継承元の関数を上書きすることでキャラ生成時のランダムパラメータの生成を防ぐ
    //RobotDataを上書き
    new void Start() { }
    new void Update() { }


    public void SetData(RobotData data)
    {
        clean = data.clean;
        battery = data.battery;
        specialSkill = data.specialSkill;
        name = data.name;
        robotSprite = data.robotSprite;

        if(cRank != null)
        {
            cRank.text = RobotManager.Instance.RankTransfer(clean);
            bRank.text = RobotManager.Instance.RankTransfer(battery);
            if(specialSkill != SPECIALSKILL_LIST.SPECIALSKILL_NULL)
            {
                skillName.text = specialSkill.ToString();
            }
            else
            {
                skillName.text = "なし";
            }
            nameText.text = data.name;

        }

        if(robotImage != null)
        {
            robotImage.sprite = data.robotSprite;
        }

        //originalGameObject = ;
    }
    public void Create(GameObject original)
    {
        GameObject root = transform.root.gameObject;
        canvas = root.transform.GetComponent<CleaningCanvas>();

        RobotData data = original.GetComponent<RobotData>();
        clean = data.clean;
        battery = data.battery;
        specialSkill = data.specialSkill;
        name = data.name;

        cRank.text = RobotManager.Instance.RankTransfer(clean);
        bRank.text = RobotManager.Instance.RankTransfer(battery);
        nameText.text = name;
        robotSprite = robotImage.sprite = data.robotSprite;

        originalGameObject = original;
    }

    public void onClick()
    {
        canvas.RobotDataBack();
    }

}