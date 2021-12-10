using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �l�ޑI���@�L�����N�^�[�p�����[�^UI�@�N���X
/// </summary>
public class RecruitCharacterData : CharacterData
{
    [SerializeField] Text nameText, rRank, pRank, mRank, iRank;

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
    void Start(){ }
    void Update(){ }

    public void SetCharacterData(RecruitCharacterData data)
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
            profileText.text = data.introduction;
            nameText.text = data.name;
        }

        if(charaImage != null)
        {
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
        nameText.text = originalData.name;
        profileText.text = originalData.introduction;
        characterSprite = originalData.characterSprite;
        name = originalData.name;

        rRank.text = CharacterManager.Instance.RankTransfer(research);
        pRank.text = CharacterManager.Instance.RankTransfer(production);
        mRank.text = CharacterManager.Instance.RankTransfer(management);
        iRank.text = CharacterManager.Instance.RankTransfer(investigation);

        original = originalGameObject;
    }
}
