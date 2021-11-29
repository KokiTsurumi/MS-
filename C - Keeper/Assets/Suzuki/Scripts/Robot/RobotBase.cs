using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotBase : MonoBehaviour
{
    // ม๊Z\
    public enum SPECIALSKILL_LIST
    {
        SPECIALSKILL_NULL,

        SPECIALSKILL_SPECIALIZED_FOR_CLEANING,  // ด|มป
        SPECIALSKILL_OIL_COLLECTION,            // ๛๑๛
        SPECIALSKILL_PLASTIC_ONLY,              // vX`bN๊p
        SPECIALSKILL_LARGE_CAPACITY_BATTERY,    // ๅeสobe[
        SPECIALSKILL_PERFECT_INVESTIGATOR,      // ฎเ๘ฒธ@
    }



    // oฯ
    [SerializeField, Range(0, 5)]
    public int clean, battery;              // p[^(clean:ด|, battery:์ฎิ)(0`5:E`S)

    public SPECIALSKILL_LIST specialSkill;  // ม๊Z\
    public string name;                     // ผO

    public Sprite robotSprite;              // {bgฬๆ
    


    // p[^ถฌึ
    public void ParamGenerator()
    {
        // ด|
        float cleanAverage = (CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().research + CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().research) / 2;
        clean = Mathf.CeilToInt(cleanAverage);

        // ์ฎิ
        float batteryAverage = (CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().management + CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().management) / 2;
        battery = Mathf.CeilToInt(batteryAverage);
    }

    // ม๊Z\ถฌึ
    public void SpecialSkillGenerator()
    {
        CharacterBase.TAG_LIST tag1, tag2;
        tag1 = CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().tag;
        tag2 = CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().tag;

        if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_CLEANING || tag1 == CharacterBase.TAG_LIST.TAG_CLEANING && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)                               // {bgHw & ด|
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_SPECIALIZED_FOR_CLEANING; // ด|มป
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_FUELOIL_COLLECTION || tag1 == CharacterBase.TAG_LIST.TAG_FUELOIL_COLLECTION && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)      // {bgHw & d๛๑๛
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION;           // ๛๑๛
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_PLASTIC_RESEARCH || tag1 == CharacterBase.TAG_LIST.TAG_PLASTIC_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)          // {bgHw & vX`bNค
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY;             // vX`bN๊p
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_BATTERY_MANUFACTURE || tag1 == CharacterBase.TAG_LIST.TAG_BATTERY_MANUFACTURE && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)    // {bgHw & obe[ปข
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY;   // ๅeสobe[
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH)                                                                                                          // ฉRฒธ๕ & ฉRฒธ๕
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_PERFECT_INVESTIGATOR;     // ฎเ๘ฒธ@
        }
        else
        {
            specialSkill = SPECIALSKILL_LIST.SPECIALSKILL_NULL;                     // ม๊Z\ณต
        }   
    }

    // ผOถฌึ
    public void NameGenerator()
    {
        string name1, name2;

        // @ฬผ
        if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_SPECIALIZED_FOR_CLEANING)    // ด|มป
            name1 = "CHSE";
        else if(specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_OIL_COLLECTION)          // ๛๑๛
            name1 = "OCOE";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_PLASTIC_ONLY)           // vX`bN๊p
            name1 = "PCOE";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_LARGE_CAPACITY_BATTERY) // ๅeสobe[
            name1 = "LCBE";
        else if (specialSkill == SPECIALSKILL_LIST.SPECIALSKILL_PERFECT_INVESTIGATOR)   // ฎเ๘ฒธ@
            name1 = "PNSE";
        else                                                                            // ม๊Z\ณต
            name1 = "IHNE";

        // ขใผ
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

        // ผO
        name = name1 + name2;
    }



    // Start is called before the first frame update
    protected void Start()
    {
        ParamGenerator();
        SpecialSkillGenerator();
        NameGenerator();

        // fobOp
        Debug.Log("ด|    F" + RobotManager.Instance.RankTransfer(clean) + "  |  ParameterF" + clean);
        Debug.Log("์ฎิF" + RobotManager.Instance.RankTransfer(battery) + "  |  ParameterF" + battery);
        Debug.Log("ม๊Z\F" + specialSkill);
        Debug.Log("ผO    F" + name);
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
}
