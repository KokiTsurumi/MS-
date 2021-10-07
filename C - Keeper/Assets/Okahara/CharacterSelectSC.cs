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
         * キャラ未選択時の処理がないので追加が必要
         * Characterにタグがあればキャラクターオブジェクトに限定できる
         * if(selectCharacterObj.tag == 'Character')とか
         */
    }
}
