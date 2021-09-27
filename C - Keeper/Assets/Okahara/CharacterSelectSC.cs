using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelectSC : MonoBehaviour
{
    [SerializeField]
    EventSystem eventSystem;



    

    [SerializeField]
    GameObject selectCharacterObj;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (eventSystem.currentSelectedGameObject != null)
                selectCharacterObj = eventSystem.currentSelectedGameObject.gameObject;
            else
                selectCharacterObj = null;
        }
    }

    public void Selected()
    {

        this.gameObject.SetActive(false);
        /*
         * �L�������I�����̏������Ȃ��̂Œǉ����K�v
         * Character�Ƀ^�O������΃L�����N�^�[�I�u�W�F�N�g�Ɍ���ł���
         * if(selectCharacterObj.tag == 'Character')�Ƃ�
         */
    }
}
