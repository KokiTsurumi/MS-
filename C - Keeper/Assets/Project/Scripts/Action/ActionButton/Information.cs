using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Information : ActionButtonInterface
{
    [SerializeField]
    GameObject infomationUI;

    override public void ActionStart()
    {
        cameraController.ZoomOut();
        cameraController.IslandChoice();
        //未調査→吹き出しアイコン表示
        //調査中→タイマー表示
        //調査完了→何かしらのアイコン表示
    }
    public override void DisplayUI()
    {
        cameraController.backButton.SetActive(true);
        canvas = (GameObject)Instantiate(infomationUI);
    }

    public override void ActionEnd()
    {
        cameraController.backButton.SetActive(false);

        //カメラを拠点島に戻す
        cameraController.TranslateCenterIsland();


        Destroy(canvas);
    }
}
