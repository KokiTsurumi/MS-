using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvestigationCanvas : SelectCanvasInterface
{
    public override void StartButton()
    {
        //�^�b�O�v�Z����
        //�^�C�}�[�v�Z����
        //���̃X�N���v�g���ɂ��钲���ς݂�bool��ture�i�����x�\���ɗ��p�j
        GameObject target = Camera.main.GetComponent<CameraController>().targetIsland;
        WorldManager.Instance.SetTimer(3.0f, target.transform.position);


        //�����J�nUI�\��

        base.StartButton();
    }
}
