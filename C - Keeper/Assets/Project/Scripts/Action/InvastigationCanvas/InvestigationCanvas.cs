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

        //�A�j���I����
        if (startAnimationCanvas.GetComponent<AnimationCallBack>().GetCallBack == true)
        {
            endToggle = true;
            startAnimationCanvas.SetActive(false);

            //���̃X�N���v�g���ɂ��钲���ς݂�bool��ture�i�����x�\���ɗ��p�j
            GameObject target = IslandManager.Instance.GetCurrentIsland();

            //�L�����N�^�[�Z�b�g
            CharacterManager.Instance.characterList[0] = selectChara[0].GetComponent<SelectCharacterDataInterface>().data.originalGameObject;
            CharacterManager.Instance.characterList[1] = selectChara[1].GetComponent<SelectCharacterDataInterface>().data.originalGameObject;            //�^�b�O�v�Z����
            float time = CharacterManager.Instance.CalcInvestigationTime();

            CharacterManager.Instance.UseCharacter();

            if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Investigation)
            {
                //�^�C�}�[�v�Z����
                target.GetComponent<IslandBase>().StartInvestigate(2.0f);//�Œ�
                Destroy(this.gameObject);

                //TutorialManager.Instance.NextStep();
            }
            else
            {
                //�^�C�}�[�v�Z����
                target.GetComponent<IslandBase>().StartInvestigate(time);

                //�J�����ړ�
                StartCoroutine(ActionEnd());
            }

        }
    }

    public override void StartButton()
    {
        

        //�L�����o�X��\��
        //cameraController.GetActionCanvas().SetActive(false);
        mainCanvas.SetActive(false);
        cameraController.backButton.SetActive(false);

        //�����J�nUI�\��
        startAnimationCanvas.SetActive(true);


        
        
    }

    IEnumerator ActionEnd()
    {
        //���b��ɃJ������߂�
        yield return new WaitForSeconds(1.0f);

        //base.StartButton();
        cameraController.ActionEnd();
    }
}
