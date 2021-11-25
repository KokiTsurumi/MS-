using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionRobotInterface : MonoBehaviour
{
    [SerializeField]
    Text nameText, cRank, bRank, skillText, skillName;

    CleaningCanvas canvas;

    string robotName;
    int clean;
    int battery;
    RobotBase.SPECIALSKILL_LIST skill;


    public bool isSelected { get; set; } = false;

    public string GetName => robotName;
    public int GetClean => clean;
    public int GetBattery => battery;

    public RobotBase.SPECIALSKILL_LIST GetSkill => skill;

    public void SetData(string name, int c,int b,RobotBase.SPECIALSKILL_LIST skl)
    {
        robotName = name;
        clean = c;
        battery = b;
        skill = skl;

        nameText.text = robotName;
        cRank.text = RobotManager.Instance.RankTransfer(c);
        bRank.text = RobotManager.Instance.RankTransfer(b);
        skillName.text = skill.ToString();

    }
    public void onClick()
    {
        canvas.RobotDataBack();
    }

    public void Create()
    {
        GameObject root = transform.root.gameObject;
        canvas = root.transform.GetComponent<CleaningCanvas>();
    }

}