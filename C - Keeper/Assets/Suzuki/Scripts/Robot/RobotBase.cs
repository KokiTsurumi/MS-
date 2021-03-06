using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotBase : MonoBehaviour
{
    /// <summary>
    /// ÁęZ\
    /// </summary>
    public enum SPECIALSKILL_LIST
    {
        SPECIALSKILL_NULL,

        SPECIALSKILL_SPECIALIZED_FOR_CLEANING,  // ´|Áģ
        SPECIALSKILL_OIL_COLLECTION,            // ûņû
        SPECIALSKILL_PLASTIC_ONLY,              // vX`bNęp
        SPECIALSKILL_LARGE_CAPACITY_BATTERY,    // åeĘobe[
        SPECIALSKILL_PERFECT_INVESTIGATOR,      // Žāø˛¸@
        SPECIALSKILL_ALMIGHTY,                  // S\
    }



    // oĪ
    [SerializeField, Range(0, 5)]
    public int clean, battery;              // p[^(clean:´|, battery:ėŽÔ)(0`5:E`S)

    public SPECIALSKILL_LIST specialSkill;  // ÁęZ\
    public string specialSkillName;         // ÁęZ\ĖŧO
    public string name;                     // ŧO

    public Sprite robotSprite;              // {bgĖæ



    /// <summary>
    /// eíp[^ļŦÖ
    /// </summary>
    public void ParamGenerator()
    {
        // ´|
        float cleanAverage = (CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().research + CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().research) / 2;
        clean = Mathf.CeilToInt(cleanAverage);

        // ėŽÔ
        float batteryAverage = (CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().management + CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().management) / 2;
        battery = Mathf.CeilToInt(batteryAverage);
    }

    /// <summary>
    /// ÁęZ\ļŦÖ
    /// </summary>
    public void SpecialSkillGenerator()
    {
        CharacterBase.TAG_LIST tag1, tag2;
        tag1 = CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().tag;
        tag2 = CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().tag;

        if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_CLEANING || tag1 == CharacterBase.TAG_LIST.TAG_CLEANING && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)                               // {bgHw & ´|
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_SPECIALIZED_FOR_CLEANING; // ´|Áģ
            specialSkillName = "´|Áģ";
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_FUELOIL_COLLECTION || tag1 == CharacterBase.TAG_LIST.TAG_FUELOIL_COLLECTION && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)      // {bgHw & dûņû
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION;           // ûņû
            specialSkillName = "ûņû";
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_PLASTIC_RESEARCH || tag1 == CharacterBase.TAG_LIST.TAG_PLASTIC_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)          // {bgHw & vX`bN¤
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY;             // vX`bNęp
            specialSkillName = "vX`bNęp";
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_BATTERY_MANUFACTURE || tag1 == CharacterBase.TAG_LIST.TAG_BATTERY_MANUFACTURE && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)    // {bgHw & obe[ģĸ
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY;   // åeĘobe[
            specialSkillName = "åeĘobe[";
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH)                                                                                                          // ŠR˛¸õ & ŠR˛¸õ
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_PERFECT_INVESTIGATOR;     // Žāø˛¸@
            specialSkillName = "Žāø˛¸@";
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_CLEANING && tag2 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH || tag1 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_CLEANING)    // {bgHw & obe[ģĸ
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_ALMIGHTY;                 // I[}CeB
            specialSkillName = "I[}CeB";
        }
        else
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_NULL;                     // ÁęZ\ŗĩ
            specialSkillName = "Čĩ";
        }
    }

    /// <summary>
    /// ŧOļŦÖ
    /// </summary>
    public void NameGenerator()
    {
        string name1, name2;

        // @Ėŧ
        if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_SPECIALIZED_FOR_CLEANING)    // ´|Áģ
            name1 = "CHS - ";
        else if(specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION)          // ûņû
            name1 = "OCO - ";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY)           // vX`bNęp
            name1 = "PCO - ";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY) // åeĘobe[
            name1 = "LCB - ";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_PERFECT_INVESTIGATOR)   // Žāø˛¸@
            name1 = "PNS - ";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_ALMIGHTY)               // I[}CeB
            name1 = "AGY - ";
        else                                                                            // ÁęZ\ŗĩ
            name1 = "IHN - ";

        // ĸãŧ
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

        // ŧO
        name = name1 + name2;
    }

    /// <summary>
    /// {bgĖæđŨčˇéÖ
    /// </summary>
    public void SetCharacterSprite()
    {
        string directoryPath = "Áę{";  // Áę{bgĖæĒüÁŊpXđwč
        Sprite[] spriteList;                // æžĩŊ{bgæđÛˇéXg

        spriteList = Resources.LoadAll<Sprite>(directoryPath);   // Áę{bgĖæđSÄæž
        
        if(specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_SPECIALIZED_FOR_CLEANING)     // ´|Áģ
        {
            foreach(Sprite sprite in spriteList)
            {
                if (sprite.name == "´|Áģ{")
                    robotSprite = sprite;
            }
        }
        else if(specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION)          // ûņû
        {
            foreach (Sprite sprite in spriteList)
            {
                if (sprite.name == "ûņû{")
                    robotSprite = sprite;
            }
        }
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY)           // vX`bNęp
        {
            foreach (Sprite sprite in spriteList)
            {
                if (sprite.name == "vX`bNęp{")
                    robotSprite = sprite;
            }
        }
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY) // åeĘobe[
        {
            foreach (Sprite sprite in spriteList)
            {
                if (sprite.name == "åeĘobe[{")
                    robotSprite = sprite;
            }
        }
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_PERFECT_INVESTIGATOR)   // Žāø˛¸@
        {
            foreach (Sprite sprite in spriteList)
            {
                if (sprite.name == "Žāø˛¸õ{")
                    robotSprite = sprite;
            }
        }
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_ALMIGHTY)               // I[}CeB
        {
            foreach (Sprite sprite in spriteList)
            {
                if (sprite.name == "I[}CeB{")
                    robotSprite = sprite;
            }
        }
        else                                                                            // ÁęZ\Čĩ
        {
            Sprite[] normalRobotList = Resources.LoadAll<Sprite>("Ęí{");   // Ęí{bgĖæđSÄæž;

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
