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
        SPECIALSKILL_ALMIGHTY,                  // 全能
    }



    // メンバ変数
    [SerializeField, Range(0, 5)]
    public int clean, battery;              // パラメータ(clean:清掃, battery:駆動時間)(0〜5:E〜S)

    public SPECIALSKILL_LIST specialSkill;  // 特殊技能
    public string specialSkillName;         // 特殊技能の名前
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
            specialSkillName = "清掃特化";
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_FUELOIL_COLLECTION || tag1 == CharacterBase.TAG_LIST.TAG_FUELOIL_COLLECTION && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)      // ロボット工学 & 重油回収
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION;           // 油回収
            specialSkillName = "油回収";
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_PLASTIC_RESEARCH || tag1 == CharacterBase.TAG_LIST.TAG_PLASTIC_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)          // ロボット工学 & プラスチック研究
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY;             // プラスチック専用
            specialSkillName = "プラスチック専用";
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_BATTERY_MANUFACTURE || tag1 == CharacterBase.TAG_LIST.TAG_BATTERY_MANUFACTURE && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)    // ロボット工学 & バッテリー製造
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY;   // 大容量バッテリー
            specialSkillName = "大容量バッテリー";
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH)                                                                                                          // 自然調査員 & 自然調査員
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_PERFECT_INVESTIGATOR;     // 完璧調査機
            specialSkillName = "完璧調査機";
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_CLEANING && tag2 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH || tag1 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_CLEANING)    // ロボット工学 & バッテリー製造
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_ALMIGHTY;                 // オールマイティ
            specialSkillName = "オールマイティ";
        }
        else
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_NULL;                     // 特殊技能無し
            specialSkillName = "なし";
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
            name1 = "CHS - ";
        else if(specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION)          // 油回収
            name1 = "OCO - ";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY)           // プラスチック専用
            name1 = "PCO - ";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY) // 大容量バッテリー
            name1 = "LCB - ";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_PERFECT_INVESTIGATOR)   // 完璧調査機
            name1 = "PNS - ";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_ALMIGHTY)               // オールマイティ
            name1 = "AGY - ";
        else                                                                            // 特殊技能無し
            name1 = "IHN - ";

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

    /// <summary>
    /// ロボットの画像を設定する関数
    /// </summary>
    public void SetCharacterSprite()
    {
        string directoryPath = "特殊ロボ";  // 特殊ロボットの画像が入ったパスを指定
        Sprite[] spriteList;                // 取得したロボット画像を保持するリスト

        spriteList = Resources.LoadAll<Sprite>(directoryPath);   // 特殊ロボットの画像を全て取得
        
        if(specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_SPECIALIZED_FOR_CLEANING)     // 清掃特化
        {
            foreach(Sprite sprite in spriteList)
            {
                if (sprite.name == "清掃特化ロボ")
                    robotSprite = sprite;
            }
        }
        else if(specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION)          // 油回収
        {
            foreach (Sprite sprite in spriteList)
            {
                if (sprite.name == "油回収ロボ")
                    robotSprite = sprite;
            }
        }
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY)           // プラスチック専用
        {
            foreach (Sprite sprite in spriteList)
            {
                if (sprite.name == "プラスチック専用ロボ")
                    robotSprite = sprite;
            }
        }
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY) // 大容量バッテリー
        {
            foreach (Sprite sprite in spriteList)
            {
                if (sprite.name == "大容量バッテリーロボ")
                    robotSprite = sprite;
            }
        }
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_PERFECT_INVESTIGATOR)   // 完璧調査機
        {
            foreach (Sprite sprite in spriteList)
            {
                if (sprite.name == "完璧調査員ロボ")
                    robotSprite = sprite;
            }
        }
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_ALMIGHTY)               // オールマイティ
        {
            foreach (Sprite sprite in spriteList)
            {
                if (sprite.name == "オールマイティロボ")
                    robotSprite = sprite;
            }
        }
        else                                                                            // 特殊技能なし
        {
            Sprite[] normalRobotList = Resources.LoadAll<Sprite>("通常ロボ");   // 通常ロボットの画像を全て取得;

            int index = Random.Range(0, normalRobotList.Length);
            robotSprite = normalRobotList[index];
        }
    }



    // Start is called before the first frame update
    protected void Start()
    {
        ParamGenerator();
        SpecialSkillGenerator();
        NameGenerator();
        SetCharacterSprite();
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
}
