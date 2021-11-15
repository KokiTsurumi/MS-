using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterData : MonoBehaviour
{

    GameObject selectGameObject = null;

    GameObject beforeSelectGameObject = null;

    public void SetData(ref GameObject obj)//�A�h���X�Q��
    {
        //�O��I�����Ă��L�����𖢑I���ɂ���
        if(beforeSelectGameObject != null)
        {
            beforeSelectGameObject.GetComponent<Button>().interactable = true;
        }

        selectGameObject = obj;

        GameObject characterImage = selectGameObject.transform.GetChild(0).gameObject;

        //�o�b�N�O���E���h
        GetComponent<Image>().color = characterImage.transform.GetChild(0).GetComponent<Image>().color;

        //�L�����N�^�[
        transform.GetChild(0).GetComponent<Image>().sprite = characterImage.transform.GetChild(1).GetComponent<Image>().sprite;

        //isSelected = true;
        selectGameObject.GetComponent<InvestigationCharacterData>().isSelected = true;

        selectGameObject.GetComponent<Button>().interactable = false;
        beforeSelectGameObject = selectGameObject;
    }

    public T GetData<T>()
    {
        if (selectGameObject.GetComponent<T>() == null)
            Debug.Log("�I�u�W�F�N�g�ɃX�N���v�g���A�^�b�`����Ă��܂���");


        return selectGameObject.GetComponent<T>();
        //return selectGameObject.GetComponent<InvestigationCharacterData>();
    }

    public GameObject GetSelectGameObject()
    {
        return selectGameObject;
    }

}
