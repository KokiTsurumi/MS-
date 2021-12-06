using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitCharacterData : MonoBehaviour
{
    [SerializeField]
    Text nameText, rRank, pRank, mRank, iRank;

    [SerializeField]
    Text profileText;

    [SerializeField]
    Image charaImage;

    [System.NonSerialized]
    public GameObject original;

    [System.NonSerialized]
    public bool selected = false;


    public void SetCharacterData(RecruitCharacterData data)
    {
        nameText.text = data.nameText.text;
        charaImage.sprite = data.charaImage.sprite;
        profileText.text = data.profileText.text;
        rRank.text = data.rRank.text;
        pRank.text = data.pRank.text;
        mRank.text = data.mRank.text;
        iRank.text = data.iRank.text;
    }

    public void SetCharacterData(GameObject obj)
    {
       
        CharacterBase data = obj.GetComponent<CharacterBase>();

        nameText.text = data.name;
        charaImage.sprite = data.characterSprite;
        profileText.text = data.introduction;
        rRank.text = CharacterManager.Instance.RankTransfer(data.research);
        pRank.text = CharacterManager.Instance.RankTransfer(data.production);
        mRank.text = CharacterManager.Instance.RankTransfer(data.management);
        iRank.text = CharacterManager.Instance.RankTransfer(data.investigation);

        original = obj;
    }
}
