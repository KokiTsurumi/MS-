using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ActionButtonInterface�N���X���p�����邱�ƂŁA�J�[�\�������݂ǂ̃{�^���̏�ɂ���̂����m���邱�Ƃ��ł���
public class Investigation : ActionButtonInterface
{
    [SerializeField]
    GameObject investigationUI;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    override public void DisplayUI() 
    {
        investigationUI.SetActive(true);
        investigationUI.transform.GetChild(0).GetComponent<InvestigationCanvas>().CreateCharaList();
    }

}
