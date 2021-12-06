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
        if (startAnimationCanvas.GetComponent<AnimationCallBack>().GetCallBack == true)
        {
            endToggle = true;
            startAnimationCanvas.SetActive(false);

            //島のスクリプト内にある調査済みのboolをture（汚染度表示に利用）
            GameObject target = IslandManager.Instance.GetCurrentIsland();

            //キャラクターセット
            CharacterData set1 = selectChara[0].GetComponent<SelectCharacterDataInterface>().data.originalGameObject.GetComponent<CharacterData>();
            CharacterData set2 = selectChara[1].GetComponent<SelectCharacterDataInterface>().data.originalGameObject.GetComponent<CharacterData>();            //タッグ計算処理
            float time = CharacterManager.Instance.CalcInvestigationTime(set1, set2);


            if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Investigation)
            {
                //タイマー計算処理
                target.GetComponent<IslandBase>().StartInvestigate(2.0f);//固定
                Destroy(this.gameObject);

                //TutorialManager.Instance.NextStep();
            }
            else
            {
                //タイマー計算処理
                target.GetComponent<IslandBase>().StartInvestigate(time);

                //カメラ移動
                StartCoroutine(ActionEnd());
            }

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
