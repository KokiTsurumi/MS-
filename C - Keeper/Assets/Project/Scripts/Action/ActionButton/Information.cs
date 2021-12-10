using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 情報ボタン　クラス
/// </summary>
public class Information : ActionButtonInterface
{

    override public void ActionStart()
    {
        cameraController.ZoomOut();
        cameraController.IslandChoice();
        
        //調査中→タイマー表示
        //調査完了→吹き出しアイコン表示

        //foreach(GameObject obj in IslandManager.Instance.islandList)
        //{
        //    //調査完了済かつ未確認の状態
        //    if (obj.GetComponent<IslandBase>().state == IslandBase.STATE_ISLAND.STATE_INVESTIGATED
        //        &&
        //        (true))//trueにIconオブジェクトのクラスで表示されたかどうかのboot値を参照
        //    {
        //        //Iconをセット
        //    }
        //}


    }
    public override void DisplayUI()
    {

        //調査完了済かつ未確認の状態
        //if()
        {
            //Icon表示
            //IslandManager.Instance.GetCurrentIsland().GetComponent<IslandBase>().//Icon表示
        }


        cameraController.backButton.SetActive(true);
    }

    public override void ActionEnd()
    {
        cameraController.backButton.SetActive(false);

        //カメラを拠点島に戻す
        cameraController.TranslateCenterIsland();


        Destroy(canvas);
    }
}
