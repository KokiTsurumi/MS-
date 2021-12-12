using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 調査ボタン　クラス
/// </summary>
public class Investigation : ActionButtonInterface
{
    [SerializeField] GameObject investigationUI;

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

}
