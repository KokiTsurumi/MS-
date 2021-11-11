using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterData : MonoBehaviour
{
    public bool isSelected { get; set; }

    InvestigationCharacterData data;

    GameObject selectGameObject;

    public void SetData(InvestigationCharacterData set,ref GameObject obj)//�A�h���X�Q��
    {
        data = set;
        selectGameObject = obj;
    }

    public InvestigationCharacterData GetData()
    {
        return data;
    }

    public GameObject GetSelectGameObject()
    {
        return selectGameObject;
    }
}
