using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvestigationCanvas : SelectCanvasInterface
{
    public override void StartButton()
    {
        //タッグ計算処理
        //タイマー計算処理
        //島のスクリプト内にある調査済みのboolをture（汚染度表示に利用）
        GameObject target = Camera.main.GetComponent<CameraController>().targetIsland;
        WorldManager.Instance.SetTimer(3.0f, target.transform.position);


        //調査開始UI表示

        base.StartButton();
    }
}
