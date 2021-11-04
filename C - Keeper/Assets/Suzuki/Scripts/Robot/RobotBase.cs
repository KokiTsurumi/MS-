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

        SPECIALSKILL_SPECIALIZED_FOR_CLEANING,
        SPECIALSKILL_OIL_COLLECTION,
        SPECIALSKILL_PLASTIC_ONLY,
        SPECIALSKILL_LARGE_CAPACITY_BATTERY,
    }



    // メンバ変数
    public int clean, battery;              // パラメータ(clean:清掃, battery:駆動時間)(0～5:E～S)
    public SPECIALSKILL_LIST specialSkill;  // 特殊技能

    public Image robotImage;                // ロボットの画像

    public GameObject CharacterManager;     // CharacterManager取得用

    // パラメータ生成関数
    public void ParamGenerator()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
