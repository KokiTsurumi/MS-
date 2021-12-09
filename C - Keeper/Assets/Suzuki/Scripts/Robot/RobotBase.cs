using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotBase : MonoBehaviour
{
    /// <summary>
    /// 特殊技能
    /// </summary>
    public enum SPECIALSKILL_LIST
    {
        SPECIALSKILL_NULL,

        SPECIALSKILL_SPECIALIZED_FOR_CLEANING,  // 清掃特化
        SPECIALSKILL_OIL_COLLECTION,            // 油回収
        SPECIALSKILL_PLASTIC_ONLY,              // プラスチック専用
        SPECIALSKILL_LARGE_CAPACITY_BATTERY,    // 大容量バッテリー
        SPECIALSKILL_PERFECT_INVESTIGATOR,      // 完璧調査機
    }



    // メンバ変数
    [SerializeField, Range(0, 5)]
    public int clean, battery;              // パラメータ(clean:清掃, battery:駆動時間)(0～5:E～S)

    public SPECIALSKILL_LIST specialSkill;  // 特殊技能
    public string name;                     // 名前

    public Sprite robotSprite;              // ロボットの画像



    /// <summary>
    /// 各種パラメータ生成関数
    /// </summary>
    public void ParamGenerator()
    {
        // 清掃
        float cleanAverage = (CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().research + CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().research) / 2;
        clean = Mathf.CeilToInt(cleanAverage);

        // 駆動時間
        float batteryAverage = (CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().management + CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().management) / 2;
        battery = Mathf.CeilToInt(batteryAverage);
    }

    /// <summary>
    /// 特殊技能生成関数
    /// </summary>
    public void SpecialSkillGenerator()
    {
        CharacterBase.TAG_LIST tag1, tag2;
        tag1 = CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().tag;
        tag2 = CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().tag;

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
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY;   // 大容量バッテリー
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH)                                                                                                          // 自然調査員 & 自然調査員
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_PERFECT_INVESTIGATOR;     // 完璧調査機
        }
        else
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_NULL;                     // 特殊技能無し
        }   
    }

    /// <summary>
    /// 名前生成関数
    /// </summary>
    public void NameGenerator()
    {
        string name1, name2;

        // 機体名
        if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_SPECIALIZED_FOR_CLEANING)    // 清掃特化
            name1 = "CHS・";
        else if(specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION)          // 油回収
            name1 = "OCO・";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY)           // プラスチック専用
            name1 = "PCO・";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY) // 大容量バッテリー
            name1 = "LCB・";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_PERFECT_INVESTIGATOR)   // 完璧調査機
            name1 = "PNS・";
        else                                                                            // 特殊技能無し
            name1 = "IHN・";

        // 世代名
        if (clean <= 0)             // E
            name2 = "Prototype";
        else if (clean == 1)        // D
            name2 = "Mk.I";
        else if (clean == 2)        // C
            name2 = "Mk.II";
        else if (clean == 3)        // B
            name2 = "Mk.III";
        else if (clean == 4)        // A
            name2 = "Mk.IV";
        else                        // S
            name2 = "ULTIMATE";

        // 名前統合
        name = name1 + name2;
    }



    // Start is called before the first frame update
    protected void Start()
    {
        ParamGenerator();
        SpecialSkillGenerator();
        NameGenerator();

        // デバッグ用
        Debug.Log("清掃    ：" + RobotManager.Instance.RankTransfer(clean) + "  |  Parameter：" + clean);
        Debug.Log("駆動時間：" + RobotManager.Instance.RankTransfer(battery) + "  |  Parameter：" + battery);
        Debug.Log("特殊技能：" + specialSkill);
        Debug.Log("名前    ：" + name);
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
}
