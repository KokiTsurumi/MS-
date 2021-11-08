using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class choiceHumanSC : MonoBehaviour
{
  
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

    }
}
