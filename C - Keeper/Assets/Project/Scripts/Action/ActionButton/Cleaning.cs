using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 清掃ボタン　クラス
/// </summary>
public class Cleaning : ActionButtonInterface
{
    [SerializeField] GameObject cleaningUI;

    override public void ActionStart()
    {
        cameraController.ZoomOut();
        cameraController.IslandChoice();
    }

    public override void DisplayUI()
    {
        cameraController.backButton.SetActive(true);
        canvas = (GameObject)Instantiate(cleaningUI);
        canvas.transform.GetComponent<CleaningCanvas>().Initialize();
    }

    public override void ActionEnd()
    {
        cameraController.backButton.SetActive(false);

        //カメラを拠点島に戻す
        cameraController.TranslateCenterIsland();

        Destroy(canvas);
    }
}

