using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectCharacterDataInterface : MonoBehaviour
{
    protected GameObject selectGameObject = null;
    protected GameObject beforeSelectGameObject = null;

    [System.NonSerialized]
    public ActionCharacterInterface data;


    virtual public void SetData(ref GameObject obj)
    {
        if(obj.GetComponent<ActionCharacterInterface>())
            data = obj.GetComponent<ActionCharacterInterface>();
        else
        {
            Debug.Log("Error:オブジェクト未選択");
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
        selectGameObject.GetComponent<ActionCharacterInterface>().isSelected = true;

        selectGameObject.GetComponent<Button>().interactable = false;
        beforeSelectGameObject = selectGameObject;
    }

    public GameObject GetSelectGameObject()
    {
        return selectGameObject;
    }

  
}