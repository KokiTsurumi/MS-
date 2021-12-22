using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// �����@UI�@�N���X
/// </summary>
public class InvestigationCanvas : SelectCanvasInterface
{
    bool endToggle = false;

    private void Update()
    {
        if (endToggle) return;

        //�A�j���I����
        
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

                //�J�����ړ�
                StartCoroutine(ActionEnd());
            }

        }
        else if (tagAnimationCanvas.GetComponent<AnimationCallBack>().GetCallBack == true)
        {
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

        //�^�b�O���o
        if (CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().tagName == CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().tagName
            &&
           CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().tagName != "�Ȃ�")
        {
            tagAnimationCanvas.SetActive(true);
            
        }
        else
        {
            //�����J�nUI�\��
            startAnimationCanvas.SetActive(true);
        }

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
