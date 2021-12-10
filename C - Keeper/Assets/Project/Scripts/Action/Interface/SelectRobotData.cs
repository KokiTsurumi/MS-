using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectRobotData : MonoBehaviour
{
    protected GameObject selectGameObject = null;

    protected GameObject beforeSelectGameObject = null;

    ActionRobotInterface data;

    virtual public void SetData(ref GameObject obj)
    {
        if (obj == null) return;
        if (obj.GetComponent<ActionRobotInterface>() != null)
            data = obj.GetComponent<ActionRobotInterface>();
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

        GameObject characterImage = selectGameObject.transform.GetChild(0).gameObject;

        //バックグラウンド
        GetComponent<Image>().color = characterImage.transform.GetChild(0).GetComponent<Image>().color;

        //キャラクター
        transform.GetChild(0).GetComponent<Image>().sprite = characterImage.transform.GetChild(1).GetComponent<Image>().sprite;

        //isSelected = true;
        selectGameObject.GetComponent<ActionRobotInterface>().isSelected = true;

        selectGameObject.GetComponent<Button>().interactable = false;
        beforeSelectGameObject = selectGameObject;
    }

    public GameObject GetSelectGameObject()
    {
        return selectGameObject;
    }

    public string GetName()
    {
        return data.GetName;
    }

    public int GetClean()
    {
        return data.GetClean;
    }

    public int GetBattery()
    {
        return data.GetBattery;
    }

    public RobotBase.SPECIALSKILL_LIST GetSkill()
    {
        return data.GetSkill;
    }

    public GameObject GetOriginal()
    {
        return data.originalGameObject;
    }
}