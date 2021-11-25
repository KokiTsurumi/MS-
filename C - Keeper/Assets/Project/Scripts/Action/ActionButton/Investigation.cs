using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Investigation : ActionButtonInterface
{
    [SerializeField]
    GameObject investigationUI;


    bool doing = false;

  
  
    override public void ActionStart() 
    {
        cameraController.ZoomOut();
        cameraController.IslandChoice();
    }

    public override void DisplayUI()
    {
        cameraController.backButton.SetActive(true);
        canvas = (GameObject)Instantiate(investigationUI);
        canvas.transform.GetComponent<InvestigationCanvas>().Initialize();

    }

    public override void ActionEnd()
    {

        cameraController.backButton.SetActive(false);

        //カメラを拠点島に戻す
        cameraController.TranslateCenterIsland();


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
