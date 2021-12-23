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

            float time = CharacterManager.Instance.CalcInvestigationTime();
            CharacterManager.Instance.UseCharacter();


            target.GetComponent<IslandBase>().timer.GetComponent<Canvas>().enabled = true;

            if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Investigation)
            {
                TutorialManager.Instance.InvestigationTimerSet(TutorialInvestigationTimerStart);

                //Destroy(this.gameObject);
            }
            else
            {
                target.GetComponent<IslandBase>().StartInvestigate(time, InvestigationEnd);

                //カメラ移動
                StartCoroutine(ActionEnd());
            }

        }
        else if (tagAnimationCanvas.GetComponent<AnimationCallBack>().GetCallBack == true)
        {
            tagAnimationCanvas.SetActive(false);
            startAnimationCanvas.SetActive(true);
        }
    }

    public override void StartButton()
    {
        
        mainCanvas.SetActive(false);
        cameraController.backButton.SetActive(false);

        CharacterManager.Instance.selectedCharacter[0] = selectChara[0].GetComponent<SelectCharacterDataInterface>().originalGameObject;
        CharacterManager.Instance.selectedCharacter[1] = selectChara[1].GetComponent<SelectCharacterDataInterface>().originalGameObject;


        if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Investigation)
            TutorialCursor.Instance.SetActive(false);

        //タッグ演出
        bool tagCheck = false;

        CharacterBase.TAG_LIST tag1, tag2;
        tag1 = CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().tag;
        tag2 = CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().tag;

        if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_CLEANING || tag1 == CharacterBase.TAG_LIST.TAG_CLEANING && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)                               // ロボット工学 & 清掃
        {
            tagCheck = true;
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_FUELOIL_COLLECTION || tag1 == CharacterBase.TAG_LIST.TAG_FUELOIL_COLLECTION && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)      // ロボット工学 & 重油回収
        {
            tagCheck = true;

        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_PLASTIC_RESEARCH || tag1 == CharacterBase.TAG_LIST.TAG_PLASTIC_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)          // ロボット工学 & プラスチック研究
        {
            tagCheck = true;

        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_BATTERY_MANUFACTURE || tag1 == CharacterBase.TAG_LIST.TAG_BATTERY_MANUFACTURE && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)    // ロボット工学 & バッテリー製造
        {
            tagCheck = true;

        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH)                                                                                                          // 自然調査員 & 自然調査員
        {
            tagCheck = true;

        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_CLEANING && tag2 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH || tag1 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_CLEANING)    // ロボット工学 & バッテリー製造
        {
            tagCheck = true;

        }
        else
        {

        }

        if (tagCheck)
        {
            tagAnimationCanvas.SetActive(true);
        }
        else
        {
            startAnimationCanvas.SetActive(true);
        }

        audioSource.PlayOneShot(startButtonSound);
    }

    IEnumerator ActionEnd()
    {
        yield return new WaitForSeconds(1.0f);

        cameraController.ActionEnd();
    }

    public void InvestigationEnd()
    {
        Name_Value.Instance.PlusResearchCount();
    }

    public void TutorialInvestigationTimerStart()
    {
        GameObject target = IslandManager.Instance.GetCurrentIsland();
        target.GetComponent<IslandBase>().StartInvestigate(2.0f, TutorialManager.Instance.NextStep);
    }
}
