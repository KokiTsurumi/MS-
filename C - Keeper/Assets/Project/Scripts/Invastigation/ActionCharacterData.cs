using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCharacterData : MonoBehaviour
{
    [SerializeField]
    Text nameText, rRank, pRank, mRank, iRank;

    //public InvestigationCanvas canvas;

    string charaName;
    int research;//研究
    int production;//生産
    int management;//管理
    int investigation;//調査

    public bool isSelected { get; set; } = false;

   

    public string GetName => charaName;
    public int GetResearch => research;
    public int GetProduction => production;
    public int GetManagement => management;
    public int GetInvestigation => investigation;

    public void SetData(string n, int r, int p, int m, int inv)
    {
        research = r;
        production = p;
        management = m;
        investigation = inv;
        charaName = n;

        rRank.text = CharacterManager.Instance.RankTransfer(r);
        pRank.text = CharacterManager.Instance.RankTransfer(p);
        mRank.text = CharacterManager.Instance.RankTransfer(m);
        iRank.text = CharacterManager.Instance.RankTransfer(inv);
        nameText.text = charaName;
    }

    virtual public void onClick()
    {
    }

    virtual public void Create(){}

}