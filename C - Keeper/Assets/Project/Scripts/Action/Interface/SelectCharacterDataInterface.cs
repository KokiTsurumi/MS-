using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// キャラクター選択　決定キャラクター　UI　インターフェース
/// </summary>
public class SelectCharacterDataInterface : ActionCharacterInterface
{
    protected GameObject selectGameObject = null;
    protected GameObject beforeSelectGameObject = null;


    new void Start() { }
    new void Update() { }

    public void SetData(GameObject obj)
    {
        if (obj == null) return;
        if (obj.GetComponent<ActionCharacterInterface>() != null)
        {
            base.SetData(obj.GetComponent<ActionCharacterInterface>());
            originalGameObject = obj.GetComponent<ActionCharacterInterface>().originalGameObject;

        }
        else
        {
            Debug.Log("Error:オブジェクト未選択");
            return;
        }

        //前回選択してたキャラを未選択にする
        if (beforeSelectGameObject != null)
        {
            beforeSelectGameObject.GetComponent<Button>().interactable = true;
        }

        selectGameObject = obj;

        selectGameObject.GetComponent<ActionCharacterInterface>().isSelected = true;

        selectGameObject.GetComponent<Button>().interactable = false;
        beforeSelectGameObject = selectGameObject;
    }

    public GameObject GetSelectGameObject()
    {
        return selectGameObject;
    }

  
}