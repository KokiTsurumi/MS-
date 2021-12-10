using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// �A�N�V�����{�^���@�C���^�[�t�F�[�X�@�N���X
/// </summary>
public class ActionButtonInterface : MonoBehaviour
{
    protected CameraController cameraController;

    protected GameObject canvas;

    protected void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>(); 
    }

    /// <summary>
    /// �A�N�V�����{�^���������ꂽ�Ƃ�
    /// </summary>
    virtual public void ActionStart() { }
    /// <summary>
    /// �A�N�V�����I��
    /// </summary>
    virtual public void ActionEnd() { }
    /// <summary>
    /// �A�N�V����UI�\��
    /// </summary>
    virtual public void DisplayUI() { }

}
