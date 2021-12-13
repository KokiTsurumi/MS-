using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 人材選択　キャラクターパラメータUI　クラス
/// </summary>
public class RecruitCharacterData : CharacterData
{
    [SerializeField] Text nameText, rRank, pRank, mRank, iRank,ageText,tagText;

    [SerializeField] Text profileText;

    [SerializeField] Image charaImage;

    
    GameObject original;
    public GameObject GetOriginal => original;

    [System.NonSerialized]
    public bool selected = false;

    bool nullCheck = false;
    public bool GetNullCheck => nullCheck;


    //継承元の関数を上書きすることでキャラ生成時のランダムパラメータの生成を防ぐ
    //CharacterDataを上書き
    new void Start(){ }
    new void Update(){ }

    public void SetCharacterData(RecruitCharacterData data)
    {
        research = data.research;
        production = data.production;
        management = data.management;
        investigation = data.investigation;
        name = data.name;
        characterSprite = data.characterSprite;
        tag = data.tag;
        if(rRank != null)
        {
            rRank.text = CharacterManager.Instance.RankTransfer(research);
            pRank.text = CharacterManager.Instance.RankTransfer(production);
            mRank.text = CharacterManager.Instance.RankTransfer(management);
            iRank.text = CharacterManager.Instance.RankTransfer(investigation);
            if(tag != TAG_LIST.TAG_NULL)
            {
                tagText.text = data.tag.ToString();

            }
            else
            {
                tagText.text = "なし";
            }
            profileText.text = data.introduction;
            ageText.text = data.age.ToString();
            nameText.text = data.name;
        }

        if(charaImage != null)
        {
            charaImage.color = new Color(1, 1, 1, 1);
            charaImage.sprite = data.characterSprite;
        }

        original = data.original;

        nullCheck = true;
    }

    public void Create(GameObject originalGameObject)
    {
        CharacterData originalData = originalGameObject.GetComponent<CharacterData>();

        research = originalData.research;
        production = originalData.production;
        management = originalData.management;
        investigation = originalData.investigation;
        introduction = originalData.introduction;
        age = originalData.age;
        nameText.text = originalData.name;
        profileText.text = originalData.introduction;
        characterSprite = originalData.characterSprite;
        name = originalData.name;

        rRank.text = CharacterManager.Instance.RankTransfer(research);
        pRank.text = CharacterManager.Instance.RankTransfer(production);
        mRank.text = CharacterManager.Instance.RankTransfer(management);
        iRank.text = CharacterManager.Instance.RankTransfer(investigation);
        ageText.text = originalData.age.ToString();
        tag = originalData.tag;
        if (tag != TAG_LIST.TAG_NULL)
        {
            tagText.text = originalData.tag.ToString();
        }
        else
        {
            tagText.text = "なし";
        }
        original = originalGameObject;
    }
}
