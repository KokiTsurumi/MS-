using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// アクションボタン　インターフェース　クラス
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
    /// アクションボタンが押されたとき
    /// </summary>
    virtual public void ActionStart() { }
    /// <summary>
    /// アクション終了
    /// </summary>
    virtual public void ActionEnd() { }
    /// <summary>
    /// アクションUI表示
    /// </summary>
    virtual public void DisplayUI() { }

}
