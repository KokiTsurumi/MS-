using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurrentSelected : MonoBehaviour
{

    // eventSystem���擾���邽�߂̕ϐ��錾
    [SerializeField] EventSystem eventSystem;

    [SerializeField] private Image image;
   
    GameObject selectedObj;


    void Update()
    {

        // �N���b�N���ꂽ�^�C�~���O�Ŕ��肷��
        if (Input.GetMouseButton(0))
        {
            // TryCatch����Null�Ή�
            try
            {
                // �q���̃R���|�[�l���g�ɃA�N�Z�X�������̂ł�������ϐ��Ɋi�[
                selectedObj = eventSystem.currentSelectedGameObject.gameObject;

                // �{�^���̎q����Text�R���|�[�l���g����text�f�[�^���擾
                image.sprite = selectedObj.GetComponentInChildren<Image>().sprite;
            }
            // ��O�����I�Ȃ��
            catch (NullReferenceException ex)
            {
                // �Ȃɂ��I������Ȃ��ꍇ��
                //image.sprite = "akiemon.png";
            }
        }
    }
}
