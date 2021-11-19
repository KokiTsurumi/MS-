using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Investigation : ActionButtonInterface
{
    [SerializeField]
    GameObject investigationUI;

    //CameraController cameraController;

    bool doing = false;

    GameObject canvas;
  
    override public void ActionStart() 
    {
        //investigationUI.SetActive(true);
        //investigationUI.transform.GetChild(0).GetComponent<InvestigationCanvas>().CreateCharaList();
        cameraController.ZoomOut();
        cameraController.IslandChoice();
    }

    public override void DisplayUI()
    {
        canvas = (GameObject)Instantiate(investigationUI);

        canvas.transform.GetChild(0).GetComponent<InvestigationCanvas>().Initialize();

    }

    public override void ActionEnd()
    {
        //カメラを拠点島に戻す
        cameraController.TranslateCenterIsland();


        //タッグ計算処理
        //タイマー計算処理
        //島のスクリプト内にある調査済みのboolをture（汚染度表示に利用）
        


        Destroy(canvas);
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
