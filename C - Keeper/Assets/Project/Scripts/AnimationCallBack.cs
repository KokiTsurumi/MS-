using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アニメーション終了時コールバック関数
/// </summary>
public class AnimationCallBack : MonoBehaviour
{
    [SerializeField]
    bool callBack = false;

    void AnimationEndCallBack()
    {
        callBack = true;
    }

    public bool GetCallBack => callBack;

    public void AnimationStart()
    {
        callBack = false;
    }
}
