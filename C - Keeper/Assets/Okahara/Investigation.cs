using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ActionButtonInterfaceクラスを継承することで、カーソルが現在どのボタンの上にあるのか検知することができる
public class Investigation : ActionButtonInterface
{
    [SerializeField]
    GameObject investigationUI;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    override public void DisplayUI() 
    {
        investigationUI.SetActive(true);
        investigationUI.transform.GetChild(0).GetComponent<InvestigationCanvas>().CreateCharaList();
    }

}
