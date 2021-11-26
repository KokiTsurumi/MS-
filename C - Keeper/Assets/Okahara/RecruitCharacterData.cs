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

    GameObject original;

    public bool selected = false;


    void Start()
    {
        selected = false;
    }

    void Update()
    {
        
    }

    public void SetCharacterData(RecruitCharacterData data)
    {
        /*
         * name
         * image
         * profile
         * researchRank
         * productionRank
         * managementRank
         * investigationRank
         */

        nameText.text = data.nameText.text;
        charaImage = data.charaImage;
        profileText.text = data.profileText.text;
        rRank.text = data.rRank.text;
        pRank.text = data.pRank.text;
        mRank.text = data.mRank.text;
        iRank.text = data.iRank.text;
        selected = data.selected;
    }

    public void SetCharacterData(GameObject obj)
    {
        /*
         * name
         * image
         * profile
         * researchRank
         * productionRank
         * managementRank
         * investigationRank
         */

        CharacterBase data = obj.GetComponent<CharacterBase>();

        nameText.text = data.name;
        charaImage.sprite = data.characterSprite;
        profileText.text = data.profile;
        rRank.text = CharacterManager.Instance.RankTransfer(data.research);
        pRank.text = CharacterManager.Instance.RankTransfer(data.production);
        mRank.text = CharacterManager.Instance.RankTransfer(data.management);
        iRank.text = CharacterManager.Instance.RankTransfer(data.investigation);

        original = obj;
    }

    public GameObject GetOriginal => original;

    public void SetOriginal(GameObject set)
    {
        original = set;
        return;
    }
}
