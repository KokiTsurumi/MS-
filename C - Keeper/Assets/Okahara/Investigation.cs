using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ActionButtonInterfaceクラスを継承することで、カーソルが現在どのボタンの上にあるのか検知することができる
public class Investigation : ActionButtonInterface
{
    [SerializeField]
    GameObject investigationUI;

    bool doing = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    override public void DisplayUI() 
    {
        Debug.Log("check");
        investigationUI.SetActive(true);
        investigationUI.transform.GetChild(0).GetComponent<InvestigationCanvas>().CreateCharaList();
    }



    /*調査UIが押されたときの処理
     * 
     *作業が完了⇒完了UI表示
     *作業中⇒タイマー表示
     */
    public void ProgressCheck()
    {
        if (doing)
        {
            //タイマー表示
        }
        else
        {
            //作業完了UI表示または何も表示しない
        }
    }
}
