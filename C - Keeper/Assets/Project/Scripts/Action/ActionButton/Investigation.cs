using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Investigation : ActionButtonInterface
{
    [SerializeField]
    GameObject investigationUI;

    //CameraController cameraController;

    bool doing = false;

    GameObject canvas;
  
    override public void ActionStart() 
    {
        //investigationUI.SetActive(true);
        //investigationUI.transform.GetChild(0).GetComponent<InvestigationCanvas>().CreateCharaList();
        cameraController.ZoomOut();
        cameraController.IslandChoice();
    }

    public override void DisplayUI()
    {
        canvas = (GameObject)Instantiate(investigationUI);

        canvas.transform.GetChild(0).GetComponent<InvestigationCanvas>().Initialize();

    }

    public override void ActionEnd()
    {
        //�J���������_���ɖ߂�
        cameraController.TranslateCenterIsland();


        //�^�b�O�v�Z����
        //�^�C�}�[�v�Z����
        //���̃X�N���v�g���ɂ��钲���ς݂�bool��ture�i�����x�\���ɗ��p�j
        


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
