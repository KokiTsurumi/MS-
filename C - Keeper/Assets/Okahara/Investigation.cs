using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ActionButtonInterface�N���X���p�����邱�ƂŁA�J�[�\�������݂ǂ̃{�^���̏�ɂ���̂����m���邱�Ƃ��ł���
public class Investigation : ActionButtonInterface
{
    [SerializeField]
    GameObject investigationUI;

    bool doing = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    override public void DisplayUI() 
    {
        Debug.Log("check");
        investigationUI.SetActive(true);
        investigationUI.transform.GetChild(0).GetComponent<InvestigationCanvas>().CreateCharaList();
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
