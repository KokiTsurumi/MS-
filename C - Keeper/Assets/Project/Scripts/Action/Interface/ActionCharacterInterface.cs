using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCharacterInterface : MonoBehaviour
{
    [SerializeField]
    protected Text nameText, rRank, pRank, mRank, iRank;

    SelectCanvasInterface canvas;

    public GameObject originalGameObject;

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

    public void SetData(string n, int r, int p, int m, int inv,GameObject original)
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
        originalGameObject = original;
    }

    public void onClick()
    {
        canvas.CharaDataBack();
    }

    public void Create() 
    {
        GameObject root = transform.root.gameObject;
        canvas = root.transform.GetComponent<SelectCanvasInterface>();
    }


}