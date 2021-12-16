using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 情報ボタン　クラス
/// </summary>
public class Information : ActionButtonInterface
{

    [SerializeField]
    GameObject informationPopPrefab;

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
        cameraController.backButton.SetActive(false);

        GameObject island = IslandManager.Instance.GetCurrentIsland();

        if(island.GetComponent<IslandBase>().icon.GetComponent<MarkIcon>().GetWatched == false)
        {
            island.GetComponent<IslandBase>().icon.GetComponent<MarkIcon>().SetWatched();
        }


        //調査完了済かつ未確認の状態
        if (island.GetComponent<IslandBase>().state >= IslandBase.STATE_ISLAND.STATE_INVESTIGATED)
        {
            
            string pollutionType = island.GetComponent<IslandBase>().problem.ToString();
            int level = island.GetComponent<IslandBase>().GetPollutionLevel();
            //Icon表示
            GameObject obj = Instantiate(informationPopPrefab);
            obj.transform.position = island.GetComponent<IslandBase>().transform.position;

            obj.GetComponent<InformationPop>().Create(island);
            //obj.GetComponent<InformationPop>().Create("【 " +island.GetComponent<IslandBase>().name + "】\n海にごみ捨てまくって汚れちゃったよ。きれいにしてちょ\n【島が抱えている問題】\n" + pollutionType, level);
            island.GetComponent<IslandBase>().icon.GetComponent<Canvas>().enabled = false;

        }


    }

    public override void ActionEnd()
    {
        cameraController.backButton.SetActive(false);

        //カメラを拠点島に戻す
        cameraController.TranslateCenterIsland();


        Destroy(canvas);
    }
}
