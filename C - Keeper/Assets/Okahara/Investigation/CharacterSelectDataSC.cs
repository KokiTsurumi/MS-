using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelectDataSC : MonoBehaviour
{
    [SerializeField]
    Text nameText, rRank,pRank,mRank,iRank;

    public ChoiceHumanCanvasSC canvas;

    string charaName;
    public string GetName => charaName;

    
    int research;//Œ¤‹†
    int production;//¶Y
    int management;//ŠÇ—
    int investigation;//’²¸

    void Start()
    {
        Button button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(canvas.SelectCharacterObject);
    }


    public int GetResearch => research;
    public int GetProduction => production;
    public int GetManagement => management;
    public int GetInvestigation => investigation;

    public void SetData(string n,int r, int p,int m,int inv)
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

    
}
