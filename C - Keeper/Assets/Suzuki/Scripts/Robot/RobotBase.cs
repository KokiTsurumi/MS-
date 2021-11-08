using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotBase : MonoBehaviour
{
    // 特殊技能
    public enum SPECIALSKILL_LIST
    {
        SPECIALSKILL_NULL,

        SPECIALSKILL_SPECIALIZED_FOR_CLEANING,  // 清掃特化
        SPECIALSKILL_OIL_COLLECTION,            // 油回収
        SPECIALSKILL_PLASTIC_ONLY,              // プラスチック専用
        SPECIALSKILL_LARGE_CAPACITY_BATTERY,    // バッテリーお化け(仮)
    }



    // メンバ変数
    [SerializeField, Range(0, 5)]
    public int clean, battery;              // パラメータ(clean:清掃, battery:駆動時間)(0～5:E～S)

    public SPECIALSKILL_LIST specialSkill;  // 特殊技能

    public Image robotImage;                // ロボットの画像
    


    // パラメータ生成関数
    public void ParamGenerator()
    {
        // 清掃
        float cleanAverage = (CharacterManager.Instance.SelectedCharacter[0].GetComponent<CharacterBase>().research + CharacterManager.Instance.SelectedCharacter[1].GetComponent<CharacterBase>().research) / 2;
        clean = Mathf.CeilToInt(cleanAverage);

        // 駆動時間
        float batteryAverage = (CharacterManager.Instance.SelectedCharacter[0].GetComponent<CharacterBase>().management + CharacterManager.Instance.SelectedCharacter[1].GetComponent<CharacterBase>().management) / 2;
        battery = Mathf.CeilToInt(batteryAverage);
    }

    // 特殊技能生成関数
    public void SpecialSkillGenerator()
    {
        CharacterBase.TAG_LIST tag1, tag2;
        tag1 = CharacterManager.Instance.SelectedCharacter[0].GetComponent<CharacterBase>().tag;
        tag2 = CharacterManager.Instance.SelectedCharacter[1].GetComponent<CharacterBase>().tag;

        if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_CLEANING || tag1 == CharacterBase.TAG_LIST.TAG_CLEANING && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)                               // ロボット工学 & 清掃
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_SPECIALIZED_FOR_CLEANING; // 清掃特化
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_FUELOIL_COLLECTION || tag1 == CharacterBase.TAG_LIST.TAG_FUELOIL_COLLECTION && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)      // ロボット工学 & 重油回収
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION;           // 油回収
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_PLASTIC_RESEARCH || tag1 == CharacterBase.TAG_LIST.TAG_PLASTIC_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)          // ロボット工学 & プラスチック研究
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY;             // プラスチック専用
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_BATTERY_MANUFACTURE || tag1 == CharacterBase.TAG_LIST.TAG_BATTERY_MANUFACTURE && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)    // ロボット工学 & バッテリー製造
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY;   // バッテリーお化け(仮)
        }
        else
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_NULL;                     // 特殊技能無し
        }   
    }



    // Start is called before the first frame update
    protected void Start()
    {
        ParamGenerator();
        SpecialSkillGenerator();

        // デバッグ用
        Debug.Log("清掃：" + RobotManager.Instance.RankTransfer(clean) + "  |  Parameter：" + clean);
        Debug.Log("駆動時間：" + RobotManager.Instance.RankTransfer(battery) + "  |  Parameter：" + battery);
        Debug.Log("特殊技能：" + specialSkill);
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
}
