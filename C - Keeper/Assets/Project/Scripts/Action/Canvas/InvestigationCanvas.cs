using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 調査　UI　クラス
/// </summary>
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

            GameObject target = IslandManager.Instance.GetCurrentIsland();

            CharacterManager.Instance.selectedCharacter[0] = selectChara[0].GetComponent<SelectCharacterDataInterface>().originalGameObject;
            CharacterManager.Instance.selectedCharacter[1] = selectChara[1].GetComponent<SelectCharacterDataInterface>().originalGameObject;
            float time = CharacterManager.Instance.CalcInvestigationTime();
            CharacterManager.Instance.UseCharacter();


            target.GetComponent<IslandBase>().timer.GetComponent<Canvas>().enabled = true;

            if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Investigation)
            {
                target.GetComponent<IslandBase>().StartInvestigate(2.0f,null);//固定

                Destroy(this.gameObject);
            }
            else
            {
                target.GetComponent<IslandBase>().StartInvestigate(time, InvestigationEnd);

                //カメラ移動
                StartCoroutine(ActionEnd());
            }

        }
    }

    public override void StartButton()
    {
        
        mainCanvas.SetActive(false);
        cameraController.backButton.SetActive(false);

        //調査開始UI表示
        startAnimationCanvas.SetActive(true);
    }

    IEnumerator ActionEnd()
    {
        yield return new WaitForSeconds(1.0f);

        cameraController.ActionEnd();
    }

    public void InvestigationEnd()
    {
        Name_Value.Instance.PlusResearchCount();
        //Name_Value.Instance.RankConfirm();
        //RankUpUI.Instance.RankUpCheck();

    }
}
