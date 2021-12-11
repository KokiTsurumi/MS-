using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 生産ボタン　クラス
/// </summary>
public class Production : ActionButtonInterface
{
    [SerializeField] GameObject productionUI;


    override public void ActionStart()
    {
        cameraController.backButton.SetActive(true);

        canvas = (GameObject)Instantiate(productionUI);
        canvas.transform.GetComponent<ProductionCanvas>().Initialize();
    }

    public override void ActionEnd()
    {
        cameraController.backButton.SetActive(false);

        //Destroy(canvas);
        //cameraController.ActionButtonRepop();
    }

}
