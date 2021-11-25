using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitCharacterData : ActionCharacterInterface
{
    [SerializeField]
    Text profileText;

    [SerializeField]
    Image charaImage;

    GameObject original;

    void Start()
    {
        
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
    }

    public void SetCharacterData(GameObject original)
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

        CharacterData data = original.GetComponent<CharacterData>();
        this.original = original;

        nameText.text = data.name;
        charaImage = data.characterImage;
        //profileText.text = data.profileText.text;
        rRank.text = CharacterManager.Instance.RankTransfer(data.research);
        pRank.text = CharacterManager.Instance.RankTransfer(data.production);
        mRank.text = CharacterManager.Instance.RankTransfer(data.management);
        iRank.text = CharacterManager.Instance.RankTransfer(data.investigation);
    }
}
