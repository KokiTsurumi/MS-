using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestigationCharacterData : MonoBehaviour
{
    [SerializeField]
    Text nameText, rRank, pRank, mRank, iRank;

    public InvestigationCanvas canvas;

    string charaName;
    public string GetName => charaName;


    int research;//Œ¤‹†
    int production;//¶ŽY
    int management;//ŠÇ—
    int investigation;//’²¸

    

    void Start()
    {
    }




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

 

    public void onClick()
    {
        canvas.CharaDataBack();
    }

    public void Create()
    {
        GameObject root = transform.root.gameObject;
        canvas = root.transform.GetChild(0).GetComponent<InvestigationCanvas>();

    }
}
