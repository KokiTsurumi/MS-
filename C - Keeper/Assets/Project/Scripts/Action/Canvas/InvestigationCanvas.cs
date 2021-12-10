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

            CharacterManager.Instance.characterList[0] = selectChara[0].GetComponent<SelectCharacterDataInterface>().data.originalGameObject;
            CharacterManager.Instance.characterList[1] = selectChara[1].GetComponent<SelectCharacterDataInterface>().data.originalGameObject;
            Debug.Log("�g��ꂽ�L������" + CharacterManager.Instance.characterList[0].GetComponent<CharacterData>().name);
            Debug.Log("�g��ꂽ�L������" + CharacterManager.Instance.characterList[1].GetComponent<CharacterData>().name);
            CharacterManager.Instance.UseCharacter();

            float time = CharacterManager.Instance.CalcInvestigationTime();


            if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Investigation)
            {
                target.GetComponent<IslandBase>().StartInvestigate(2.0f);//�Œ�

                Destroy(this.gameObject);
            }
            else
            {
                target.GetComponent<IslandBase>().StartInvestigate(time);

                //�J�����ړ�
                StartCoroutine(ActionEnd());
            }

        }
    }

    public override void StartButton()
    {
        
        mainCanvas.SetActive(false);
        cameraController.backButton.SetActive(false);

        //�����J�nUI�\��
        startAnimationCanvas.SetActive(true);
        
    }

    IEnumerator ActionEnd()
    {
        yield return new WaitForSeconds(1.0f);

        cameraController.ActionEnd();
    }
}
