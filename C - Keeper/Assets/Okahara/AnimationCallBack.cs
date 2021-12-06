using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCallBack : MonoBehaviour
{
    bool callBack = false;

    void AnimationEndCallBack()
    {
        callBack = true;
    }

    public bool GetCallBack => callBack;
}
