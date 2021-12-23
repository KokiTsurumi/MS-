using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 生産フロー　ロボットパラメータUI　クラス
/// </summary>
public class CreateRobotData : MonoBehaviour
{
    [SerializeField] Text nameText;

    [SerializeField] Text seisouRankText;

    [SerializeField] Text seinouRankText;

    [SerializeField] Text skillText;

    [SerializeField] Image robotImage;

    public void SetData(string name,int seisou,int seinou,RobotBase.SPECIALSKILL_LIST skill,Sprite sprite)
    {
        nameText.text = name;
        seinouRankText.text = RobotManager.Instance.RankTransfer(seinou);
        seinouRankText.color = SetRankColor(seinouRankText.text);

        seisouRankText.text = RobotManager.Instance.RankTransfer(seisou);
        seisouRankText.color = SetRankColor(seisouRankText.text);

        skillText.text = skill.ToString();
        robotImage.sprite = sprite;
    }

    Color SetRankColor(string rank)
    {
        Color color = Color.white;
        switch (rank)
        {
            case "A":
                color = Color.red;
                break;
            case "B":
                color = Color.green;
                break;
            case "C":
                color = Color.blue;
                break;
            case "D":
                color = Color.magenta;
                break;
            case "E":
                color = Color.black;
                break;
            case "S":
                color = new Color32(255, 165, 0, 255);
                break;
            default:
                color = Color.black;
                break;
        }

        return color;
    }
}
