using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectCharacterDataInterface : MonoBehaviour
{
    protected GameObject selectGameObject = null;

    protected GameObject beforeSelectGameObject = null;

    ActionCharacterInterface data;

    virtual public void SetData(ref GameObject obj)
    {
        data = obj.GetComponent<ActionCharacterInterface>();

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
        selectGameObject.GetComponent<ActionCharacterInterface>().isSelected = true;

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

    public int GetResearch()
    {
        return data.GetResearch;
    }

    public int GetProduction()
    {
        return data.GetProduction;
    }

    public int GetManagement()
    {
        return data.GetManagement;
    }

    public int GetInvestigation()
    {
        return data.GetInvestigation;
    }
}