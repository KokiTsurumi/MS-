using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キャラクター紹介文データのjsonファイル取得用クラス
/// </summary>
[System.Serializable]
public class InputJsonIntroductionList
{
    public string[] Null;
    public string[] Robotics; 
    public string[] Cleaning;
    public string[] NatureResearch;
    public string[] PlasticManufacture;
    public string[] FueloilCollection;
    public string[] PlasticResearch;
    public string[] BatteryManufacture;
}

/// <summary>
/// キャラクター名前データのjsonファイル取得用クラス
/// </summary>
[System.Serializable]
public class InputJsonNameList
{
    public string[] FamilyNameList;
    public string[] FirstNameList;
}

public class CharacterBase : MonoBehaviour
{
    /// <summary>
    /// タッグ機能の種類
    /// </summary>
    public enum TAG_LIST
    {
        TAG_NULL,                   // 特殊技能なし

        TAG_ROBOTICS,               // ロボット工学
        TAG_CLEANING,               // 清掃
        TAG_NATURE_RESEARCH,        // 自然調査
        TAG_PLASTIC_MANUFACTURE,    // プラスチック製造
        TAG_FUELOIL_COLLECTION,     // 重油回収
        TAG_PLASTIC_RESEARCH,       // プラスチック研究
        TAG_BATTERY_MANUFACTURE,    // バッテリー製造

        TAG_MAX,                    // タッグ機能の最大要素数
    }



    // メンバ変数
    [SerializeField, Range(0, 5)]
    public int research, production, management, investigation;    // パラメータ(research:研究, production:生産, management:管理, investigation:調査)(0～5:E～S)

    public string name;                                            // 名前
    public int age;                                                // 年齢
    public TAG_LIST tag = TAG_LIST.TAG_NULL;                       // タッグ機能
    public string tagName;                                         // タッグの名前
    public Sprite characterSprite;                                 // キャラクターの画像

    public string introduction;                                    // 紹介文

    public int popularityRank;                                     // 知名度ランク



    /// <summary>
    /// 各種パラメータ生成関数
    /// </summary>
    public void ParamGenerator()
    {
        // ランクによって変動
        if (popularityRank <= 1)
        {
            research = Random.Range(0, 2);
            production = Random.Range(0, 2);
            management = Random.Range(0, 2);
            investigation = Random.Range(0, 2);
        }
        else if (popularityRank == 2)
        {
            research = Random.Range(0, 3);
            production = Random.Range(0, 3);
            management = Random.Range(0, 3);
            investigation = Random.Range(0, 3);
        }
        else if (popularityRank == 3)
        {
            research = Random.Range(1, 4);
            production = Random.Range(1, 4);
            management = Random.Range(1, 4);
            investigation = Random.Range(1, 4);
        }
        else if (popularityRank == 4)
        {
            research = Random.Range(2, 5);
            production = Random.Range(2, 5);
            management = Random.Range(2, 5);
            investigation = Random.Range(2, 5);
        }
        else
        {
            research = Random.Range(3, 6);
            production = Random.Range(3, 6);
            management = Random.Range(3, 6);
            investigation = Random.Range(3, 6);
        }
    }

    /// <summary>
    /// パラメータセット関数
    /// </summary>
    /// <param name="r">研究(0～5:E～S)</param>
    /// <param name="p">生産(0～5:E～S)</param>
    /// <param name="m">管理(0～5:E～S)</param>
    /// <param name="i">調査(0～5:E～S)</param>
    public void SetParam(int r, int p, int m, int i)
    {
        research        = Mathf.Clamp(r, 0, 5);
        production      = Mathf.Clamp(p, 0, 5);
        management      = Mathf.Clamp(m, 0, 5);
        investigation   = Mathf.Clamp(i, 0, 5);
    }

    /// <summary>
    /// 名前生成関数
    /// </summary>
    public void NameGenerator()
    {
        // jsonファイルから名前データを読み込み
        string inputString = Resources.Load<TextAsset>("jsonFiles/jsonNameList").ToString();
        InputJsonNameList inputJson = JsonUtility.FromJson<InputJsonNameList>(inputString);     // デシリアライズ

        // ランダム数生成
        int rand1 = Random.Range(0, inputJson.FirstNameList.Length);    // 名
        int rand2 = Random.Range(0, inputJson.FamilyNameList.Length);   // 姓

        // 名前合成
        name = inputJson.FamilyNameList[rand1] + " " + inputJson.FirstNameList[rand2];

        // デバッグ用
        //Debug.Log(name);
        //Debug.Log(inputJson.FirstNameList.Length);
        //Log(inputJson.FamilyNameList.Length);
    }

    /// <summary>
    /// 年齢生成関数
    /// </summary>
    public void AgeGenerator()
    {
        age = Random.Range(18, 61);
    }

    /// <summary>
    /// タッグ機能生成関数
    /// </summary>
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

        // タッグの種類からタッグの名前を設定
        if (tag == TAG_LIST.TAG_ROBOTICS)
            tagName = "ロボット工学";
        else if (tag == TAG_LIST.TAG_CLEANING)
            tagName = "元清掃員";
        else if (tag == TAG_LIST.TAG_NATURE_RESEARCH)
            tagName = "自然調査員";
        else if (tag == TAG_LIST.TAG_PLASTIC_MANUFACTURE)
            tagName = "プラスチック製造";
        else if (tag == TAG_LIST.TAG_FUELOIL_COLLECTION)
            tagName = "重油回収";
        else if (tag == TAG_LIST.TAG_PLASTIC_RESEARCH)
            tagName = "プラスチック研究";
        else if (tag == TAG_LIST.TAG_BATTERY_MANUFACTURE)
            tagName = "バッテリー製造";
        else
            tagName = "なし";
    }

    /// <summary>
    /// キャラクターの画像を設定する関数
    /// </summary>
    public void SetCharacterSprite()
    {
        string directoryPath = "人材NPC";   // キャラクター画像の入ったパスを指定
        Sprite[] spriteList;                // 取得したキャラクター画像を保持するリスト

        spriteList = Resources.LoadAll<Sprite>(directoryPath);   // キャラクター画像を全て取得

        // キャラクター画像をランダムで決定
        int index = Random.Range(0, spriteList.Length);
        characterSprite = spriteList[index];
    }

    /// <summary>
    /// 紹介文を生成する関数
    /// </summary>
    public void IntroductionGenerator()
    {
        // jsonファイルから紹介文データを読み込み
        string inputString = Resources.Load<TextAsset>("jsonFiles/jsonIntroductionList").ToString();
        InputJsonIntroductionList inputJson = JsonUtility.FromJson<InputJsonIntroductionList>(inputString);     // デシリアライズ
        
        // タッグ機能から紹介文を決める
        if(tag == TAG_LIST.TAG_ROBOTICS)
        {
            int rand1 = Random.Range(0, inputJson.Robotics.Length);
            introduction = inputJson.Robotics[rand1];
        }
        else if(tag == TAG_LIST.TAG_CLEANING)
        {
            int rand1 = Random.Range(0, inputJson.Cleaning.Length);
            introduction = inputJson.Cleaning[rand1];
        }
        else if (tag == TAG_LIST.TAG_NATURE_RESEARCH)
        {
            int rand1 = Random.Range(0, inputJson.NatureResearch.Length);
            introduction = inputJson.NatureResearch[rand1];
        }
        else if (tag == TAG_LIST.TAG_PLASTIC_MANUFACTURE)
        {
            int rand1 = Random.Range(0, inputJson.PlasticManufacture.Length);
            introduction = inputJson.PlasticManufacture[rand1];
        }
        else if (tag == TAG_LIST.TAG_FUELOIL_COLLECTION)
        {
            int rand1 = Random.Range(0, inputJson.FueloilCollection.Length);
            introduction = inputJson.FueloilCollection[rand1];
        }
        else if (tag == TAG_LIST.TAG_PLASTIC_RESEARCH)
        {
            int rand1 = Random.Range(0, inputJson.PlasticResearch.Length);
            introduction = inputJson.PlasticResearch[rand1];
        }
        else if (tag == TAG_LIST.TAG_BATTERY_MANUFACTURE)
        {
            int rand1 = Random.Range(0, inputJson.BatteryManufacture.Length);
            introduction = inputJson.BatteryManufacture[rand1];
        }
        else
        {
            int rand1 = Random.Range(0, inputJson.Null.Length);
            introduction = inputJson.Null[rand1];
        }
    }



    // Start is called before the first frame update
    protected void Start()
    {
        // 知名度ランク取得
        popularityRank = WorldManager.Instance.GetPopularityRank();

        // 各種生成
        ParamGenerator();
        NameGenerator();
        AgeGenerator();
        TagGenerator();
        SetCharacterSprite();
        IntroductionGenerator();
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
}
