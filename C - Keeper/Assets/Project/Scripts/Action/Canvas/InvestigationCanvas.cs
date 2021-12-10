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

            CharacterManager.Instance.characterList[0] = selectChara[0].GetComponent<SelectCharacterDataInterface>().data.originalGameObject;
            CharacterManager.Instance.characterList[1] = selectChara[1].GetComponent<SelectCharacterDataInterface>().data.originalGameObject;
            Debug.Log("使われたキャラは" + CharacterManager.Instance.characterList[0].GetComponent<CharacterData>().name);
            Debug.Log("使われたキャラは" + CharacterManager.Instance.characterList[1].GetComponent<CharacterData>().name);
            CharacterManager.Instance.UseCharacter();

            float time = CharacterManager.Instance.CalcInvestigationTime();


            if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Investigation)
            {
                target.GetComponent<IslandBase>().StartInvestigate(2.0f);//固定

                Destroy(this.gameObject);
            }
            else
            {
                target.GetComponent<IslandBase>().StartInvestigate(time);

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
}
