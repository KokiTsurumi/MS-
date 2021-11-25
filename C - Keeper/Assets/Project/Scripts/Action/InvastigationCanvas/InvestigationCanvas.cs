using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvestigationCanvas : SelectCanvasInterface
{
    bool endToggle = false;

    private void Update()
    {
        if (endToggle) return;

        //アニメ終了時
        if (startAnimationCanvas.GetComponent<ActionStartAnimatinUI>().GetCallBack == true)
        {
            endToggle = true;
            startAnimationCanvas.SetActive(false);

            //島のスクリプト内にある調査済みのboolをture（汚染度表示に利用）
            GameObject target = IslandManager.Instance.GetCurrentIsland();

            //タッグ計算処理
            //target.GetComponent<IslandBase>()
            //タイマー計算処理
            target.GetComponent<IslandBase>().StartInvestigate(5.0f);//5.0fは仮


            //カメラ移動
            StartCoroutine(ActionEnd());
        }
    }

    public override void StartButton()
    {
        

        //キャンバス非表示
        //cameraController.GetActionCanvas().SetActive(false);
        mainCanvas.SetActive(false);
        cameraController.backButton.SetActive(false);

        //調査開始UI表示
        startAnimationCanvas.SetActive(true);


        
        
    }

    IEnumerator ActionEnd()
    {
        //数秒後にカメラを戻す
        yield return new WaitForSeconds(1.0f);

        //base.StartButton();
        cameraController.ActionEnd();
    }
}
