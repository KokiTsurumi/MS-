using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キャラクター選択　UI　インターフェース　クラス
/// </summary>
public class ActionCharacterInterface : CharacterData
{
    SelectCanvasInterface canvas;

    [SerializeField] protected Text nameText, rRank, pRank, mRank, iRank;
    [SerializeField] Image charaImage;

    [SerializeField] public GameObject originalGameObject;

    public bool isSelected { get; set; } = false;

    //継承元の関数を上書きすることでキャラ生成時のランダムパラメータの生成を防ぐ
    //CharacterDataを上書き
    new void Start(){}
    new void Update(){}

    public void SetData(CharacterData data)
    {
        research = data.research;
        production = data.production;
        management = data.management;
        investigation = data.investigation;
        name = data.name;
        characterSprite = data.characterSprite;

        if(rRank != null)
        {
            rRank.text = CharacterManager.Instance.RankTransfer(research);
            rRank.color = SetRankColor(rRank.text);

            pRank.text = CharacterManager.Instance.RankTransfer(production);
            pRank.color = SetRankColor(pRank.text);

            mRank.text = CharacterManager.Instance.RankTransfer(management);
            mRank.color = SetRankColor(mRank.text);

            iRank.text = CharacterManager.Instance.RankTransfer(investigation);
            iRank.color = SetRankColor(iRank.text);


            nameText.text = data.name;
        }

        if (charaImage != null)
        {
            charaImage.color = new Color(1,1,1,1);
            charaImage.sprite = data.characterSprite;
        }

    }

    public void Create(GameObject original)
    {

        GameObject root = transform.root.gameObject;
        canvas = root.transform.GetComponent<SelectCanvasInterface>();



        CharacterData data = original.GetComponent<CharacterData>();
        research = data.research;
        production = data.production;
        management = data.management;
        investigation = data.investigation;
        name = data.name;

        rRank.text = CharacterManager.Instance.RankTransfer(research);
        rRank.color = SetRankColor(rRank.text);

        pRank.text = CharacterManager.Instance.RankTransfer(production);
        pRank.color = SetRankColor(pRank.text);

        mRank.text = CharacterManager.Instance.RankTransfer(management);
        mRank.color = SetRankColor(mRank.text);

        iRank.text = CharacterManager.Instance.RankTransfer(investigation);
        iRank.color = SetRankColor(iRank.text);


        nameText.text = name;
        characterSprite = charaImage.sprite = data.characterSprite;
        
        originalGameObject = original;
    }

    public void onClick()
    {
        canvas.CharaDataBack();
    }


    Color SetRankColor(string rank)
    {
        Color color = Color.white;
        switch(rank)
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
                color = Color.yellow;
                break;
            default:
                color = Color.black;
                break;
        }

        return color;
    }
}