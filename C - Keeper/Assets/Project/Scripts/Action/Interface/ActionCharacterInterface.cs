using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCharacterInterface : CharacterData
{
    SelectCanvasInterface canvas;

    [SerializeField]
    protected Text nameText, rRank, pRank, mRank, iRank;
    [SerializeField]
    Image charaImage;

    [System.NonSerialized]
    public GameObject originalGameObject;

    //string charaName;
    //int research;//研究
    //int production;//生産
    //int management;//管理
    //int investigation;//調査

    public bool isSelected { get; set; } = false;

    //[System.NonSerialized]
    //public string GetName => charaName;
    //public int GetResearch => research;
    //public int GetProduction => production;
    //public int GetManagement => management;
    //public int GetInvestigation => investigation;
    //public Sprite GetSprite => charaImage.sprite;

   
    void Start()
    {

    }

    void Update()
    {

    }

    //public void SetData(string n, int r, int p, int m, int inv,GameObject original,Sprite sprite)
    //{
    //    research = r;
    //    production = p;
    //    management = m;
    //    investigation = inv;
    //    charaName = n;

    //    rRank.text = CharacterManager.Instance.RankTransfer(r);
    //    pRank.text = CharacterManager.Instance.RankTransfer(p);
    //    mRank.text = CharacterManager.Instance.RankTransfer(m);
    //    iRank.text = CharacterManager.Instance.RankTransfer(inv);
    //    nameText.text = charaName;
    //    charaImage.sprite = sprite;
    //    originalGameObject = original;
    //}

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
            //profileText.text = data.introduction;
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

    //public void Create() 
    //{
        
    //}


}