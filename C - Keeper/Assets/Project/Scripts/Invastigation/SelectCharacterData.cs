using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterData : MonoBehaviour
{

    GameObject selectGameObject = null;

    GameObject beforeSelectGameObject = null;

    public void SetData(ref GameObject obj)//アドレス参照
    {
        //前回選択してたキャラを未選択にする
        if(beforeSelectGameObject != null)
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
        selectGameObject.GetComponent<InvestigationCharacterData>().isSelected = true;

        selectGameObject.GetComponent<Button>().interactable = false;
        beforeSelectGameObject = selectGameObject;
    }

    public T GetData<T>()
    {
        if (selectGameObject.GetComponent<T>() == null)
            Debug.Log("オブジェクトにスクリプトがアタッチされていません");


        return selectGameObject.GetComponent<T>();
        //return selectGameObject.GetComponent<InvestigationCharacterData>();
    }

    public GameObject GetSelectGameObject()
    {
        return selectGameObject;
    }

}
