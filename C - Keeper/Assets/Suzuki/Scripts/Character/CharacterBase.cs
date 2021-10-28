using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// jsonファイル取得用クラス
[System.Serializable]
public class InputJson
{
    public string[] FamilyNameList; 
    public string[] FirstNameList;
}

public class CharacterBase : MonoBehaviour
{
    // タッグ機能の種類
    public enum TAG_LIST
    {
        TAG_NULL,               // 特殊技能なし

        TAG_ROBOTICS,           // ロボット工学
        TAG_CLEANING,           // 清掃
        TAG_NATURERESEARCH,     // 自然調査
        TAG_PLASTICMANUFACTURE, // プラスチック製造
        TAG_FUELOILCOLLECTION,  // 重油回収

        TAG_MAX,                // タッグ機能の最大要素数
    }

    // メンバ変数
    [SerializeField, Range(0, 5)]
    public int          research, production, management, investigation;    // パラメータ(research:研究, production:生産, management:管理, investigation:調査)(0～5:E～S)

    public string       name;                                               // 名前
    public int          age;                                                // 年齢
    public TAG_LIST     tag = TAG_LIST.TAG_NULL;                            // タッグ機能
    public float        productionSpeed;                                    // 生産速度(秒)
    public float        investigationSpeed;                                 // 調査速度(秒)
    public Image        CharacterImage;                                     // キャラクターの画像

    public int          popularityRank;                                     // 知名度ランク


    // 各種パラメータ生成関数
    public void ParamGenerator()
    {
        // ランクによって変動
        if (popularityRank <= 1)
        {
            research        = Random.Range(0, 2);
            production      = Random.Range(0, 2);
            management      = Random.Range(0, 2);
            investigation   = Random.Range(0, 2);
        }
        else if (popularityRank == 2)
        {
            research        = Random.Range(0, 3);
            production      = Random.Range(0, 3);
            management      = Random.Range(0, 3);
            investigation   = Random.Range(0, 3);
        }
        else if (popularityRank == 3)
        {
            research        = Random.Range(1, 4);
            production      = Random.Range(1, 4);
            management      = Random.Range(1, 4);
            investigation   = Random.Range(1, 4);
        }
        else if (popularityRank == 4)
        {
            research        = Random.Range(2, 5);
            production      = Random.Range(2, 5);
            management      = Random.Range(2, 5);
            investigation   = Random.Range(2, 5);
        }
        else
        {
            research        = Random.Range(3, 6);
            production      = Random.Range(3, 6);
            management      = Random.Range(3, 6);
            investigation   = Random.Range(3, 6);
        }
    }

    // 名前生成関数
    public void NameGenerator()
    {
        // jsonファイルから名前データを読み込み
        string      inputString = Resources.Load<TextAsset>("jsontest").ToString();
        InputJson   inputJson   = JsonUtility.FromJson<InputJson>(inputString);     // デシリアライズ

        // ランダム数生成
        int rand1 = Random.Range(0, inputJson.FirstNameList.Length);    // 名
        int rand2 = Random.Range(0, inputJson.FamilyNameList.Length);   // 姓

        // 名前合成
        name = inputJson.FirstNameList[rand1] + " " + inputJson.FamilyNameList[rand2];

        // デバッグ用
        //Debug.Log(name);
        //Debug.Log(inputJson.FirstNameList.Length);
        //Log(inputJson.FamilyNameList.Length);
    }

    // 年齢生成関数
    public void AgeGenerator()
    {
        age = Random.Range(0, 101);
    }

    // タッグ機能生成関数
    public void TagGenerator()
    {
        float percentage = Random.Range(0f, 100f);

        // ランクによって変動
        if (popularityRank <= 1)
        {
            tag = TAG_LIST.TAG_NULL;
        }
        else if (popularityRank == 2)
        {
            if (percentage <= Random.Range(0f, 25f))    // 0～25%
                tag = (TAG_LIST)Random.Range(1, (int)TAG_LIST.TAG_MAX); // タッグ機能をランダムに決定
            else
                tag = TAG_LIST.TAG_NULL;
        }
        else if (popularityRank == 3)
        {
            if (percentage <= Random.Range(25f, 50f))   // 25～50%
                tag = (TAG_LIST)Random.Range(1, (int)TAG_LIST.TAG_MAX);
            else
                tag = TAG_LIST.TAG_NULL;
        }
        else if (popularityRank == 4)
        {
            if (percentage <= Random.Range(50f, 75f))   // 50～75%
                tag = (TAG_LIST)Random.Range(1, (int)TAG_LIST.TAG_MAX);
            else
                tag = TAG_LIST.TAG_NULL;
        }
        else
        {
            if (percentage <= Random.Range(75f, 100f))  // 75～100%
                tag = (TAG_LIST)Random.Range(1, (int)TAG_LIST.TAG_MAX);
            else
                tag = TAG_LIST.TAG_NULL;
        }
    }





    // Start is called before the first frame update
    protected void Start()
    {
        // 知名度ランク取得
        popularityRank = GameObject.Find("WorldManager").GetComponent<WorldManager>().GetPopularityRank();


        ParamGenerator();
        NameGenerator();
        AgeGenerator();
        TagGenerator();

        Debug.Log(research);
        Debug.Log(production);
        Debug.Log(management);
        Debug.Log(investigation);
        Debug.Log(name);
        Debug.Log(age);
        Debug.Log(tag);

    }

    // Update is called once per frame
    protected void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            popularityRank = GameObject.Find("WorldManager").GetComponent<WorldManager>().GetPopularityRank();

            ParamGenerator();
            NameGenerator();
            AgeGenerator();
            TagGenerator();

            Debug.Log(research);
            Debug.Log(production);
            Debug.Log(management);
            Debug.Log(investigation);
            Debug.Log(name);
            Debug.Log(age);
            Debug.Log(tag);
        }
    }
}
