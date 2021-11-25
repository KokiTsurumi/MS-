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
        if (startAnimationCanvas.GetComponent<ActionStartAnimatinUI>().GetCallBack == true)
        {
            endToggle = true;
            startAnimationCanvas.SetActive(false);

            //���̃X�N���v�g���ɂ��钲���ς݂�bool��ture�i�����x�\���ɗ��p�j
            GameObject target = IslandManager.Instance.GetCurrentIsland();

            //�^�b�O�v�Z����
            //target.GetComponent<IslandBase>()
            //�^�C�}�[�v�Z����
            target.GetComponent<IslandBase>().StartInvestigate(5.0f);//5.0f�͉�


            //�J�����ړ�
            StartCoroutine(ActionEnd());
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
