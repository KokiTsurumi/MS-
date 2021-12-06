using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRobotData : MonoBehaviour
{
    [SerializeField]
    Text nameText;

    [SerializeField]
    Text seisouRankText;

    [SerializeField]
    Text seinouRankText;


    [SerializeField]
    Text skillText;

    [SerializeField]
    Image robotImage;

    public void SetData(string name,int seisou,int seinou,RobotBase.SPECIALSKILL_LIST skill,Sprite sprite)
    {
        nameText.text = name;
        seinouRankText.text = RobotManager.Instance.RankTransfer(seinou);
        seisouRankText.text = RobotManager.Instance.RankTransfer(seisou);
        skillText.text = skill.ToString();
        robotImage.sprite = sprite;
    }
}
