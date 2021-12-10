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

    [System.NonSerialized] public GameObject originalGameObject;

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
            pRank.text = CharacterManager.Instance.RankTransfer(production);
            mRank.text = CharacterManager.Instance.RankTransfer(management);
            iRank.text = CharacterManager.Instance.RankTransfer(investigation);
            nameText.text = data.name;
        }

        if (charaImage != null)
        {
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
        pRank.text = CharacterManager.Instance.RankTransfer(production);
        mRank.text = CharacterManager.Instance.RankTransfer(management);
        iRank.text = CharacterManager.Instance.RankTransfer(investigation);
        nameText.text = name;
        characterSprite = charaImage.sprite = data.characterSprite;
        
        originalGameObject = original;
    }

    public void onClick()
    {
        canvas.CharaDataBack();
    }
}