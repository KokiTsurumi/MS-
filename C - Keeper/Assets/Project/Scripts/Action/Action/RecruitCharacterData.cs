using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �l�ޑI���@�L�����N�^�[�p�����[�^UI�@�N���X
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


    //�p�����̊֐����㏑�����邱�ƂŃL�����������̃����_���p�����[�^�̐�����h��
    //CharacterData���㏑��
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
            rRank.color = SetRankColor(rRank.text);

            pRank.text = CharacterManager.Instance.RankTransfer(production);
            pRank.color = SetRankColor(pRank.text);

            mRank.text = CharacterManager.Instance.RankTransfer(management);
            mRank.color = SetRankColor(mRank.text);

            iRank.text = CharacterManager.Instance.RankTransfer(investigation);
            iRank.color = SetRankColor(iRank.text);


            if (tag != TAG_LIST.TAG_NULL)
            {
                tagText.text = data.tag.ToString();

            }
            else
            {
                tagText.text = "�Ȃ�";
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
        rRank.color = SetRankColor(rRank.text);

        pRank.text = CharacterManager.Instance.RankTransfer(production);
        pRank.color = SetRankColor(pRank.text);

        mRank.text = CharacterManager.Instance.RankTransfer(management);
        mRank.color = SetRankColor(mRank.text);

        iRank.text = CharacterManager.Instance.RankTransfer(investigation);
        iRank.color = SetRankColor(iRank.text);


        ageText.text = originalData.age.ToString();
        tag = originalData.tag;
        if (tag != TAG_LIST.TAG_NULL)
        {
            tagText.text = originalData.tag.ToString();
        }
        else
        {
            tagText.text = "�Ȃ�";
        }
        original = originalGameObject;
    }

    Color SetRankColor(string rank)
    {
        Color color = Color.white;
        switch (rank)
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
