using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ロボット選択　決定ロボット　UI　インターフェース　クラス　
/// </summary>
public class SelectRobotData : ActionRobotInterface
{
    protected GameObject selectGameObject = null;
    protected GameObject beforeSelectGameObject = null;

    //ActionRobotInterface data;

    new void Start() { }
    new void Update() { }

    virtual public void SetData(GameObject obj)
    {
        if (obj == null) return;
        if (obj.GetComponent<ActionRobotInterface>() != null)
        {
            base.SetData(obj.GetComponent<ActionRobotInterface>());
            originalGameObject = obj.GetComponent<ActionRobotInterface>().originalGameObject;
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

        
        selectGameObject.GetComponent<ActionRobotInterface>().isSelected = true;

        selectGameObject.GetComponent<Button>().interactable = false;
        beforeSelectGameObject = selectGameObject;
    }

    public GameObject GetSelectGameObject()
    {
        return selectGameObject;
    }

  
}