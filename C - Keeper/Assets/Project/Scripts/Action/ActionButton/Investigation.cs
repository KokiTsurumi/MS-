using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Investigation : ActionButtonInterface
{
    [SerializeField]
    GameObject investigationUI;


    bool doing = false;

  
  
    override public void ActionStart() 
    {
        cameraController.ZoomOut();
        cameraController.IslandChoice();
    }

    public override void DisplayUI()
    {
        cameraController.backButton.SetActive(true);
        canvas = (GameObject)Instantiate(investigationUI);
        canvas.transform.GetComponent<InvestigationCanvas>().Initialize();

    }

    public override void ActionEnd()
    {

        cameraController.backButton.SetActive(false);

        //�J���������_���ɖ߂�
        cameraController.TranslateCenterIsland();


        Destroy(canvas);
    }

    /*����UI�������ꂽ�Ƃ��̏���
     * 
     *��Ƃ������ˊ���UI�\��
     *��ƒ��˃^�C�}�[�\��
     */
    public void ProgressCheck()
    {
        if (doing)
        {
            //�^�C�}�[�\��
        }
        else
        {
            //��Ɗ���UI�\���܂��͉����\�����Ȃ�
        }
    }
}
