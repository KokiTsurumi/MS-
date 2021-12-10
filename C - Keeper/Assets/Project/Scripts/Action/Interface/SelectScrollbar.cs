using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スクロールバー　クラス
/// </summary>
public class SelectScrollbar : MonoBehaviour
{
    const float value = 1.0f;
    bool set = false;

    void LateUpdate()
    {
        if(set)
        {
            this.GetComponent<Scrollbar>().value = value;
            set = false;
        }
    }

    public void ScrollbarPositionReset()
    {
        set = true;
    }
}
