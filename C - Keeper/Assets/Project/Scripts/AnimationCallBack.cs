using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�j���[�V�����I�����R�[���o�b�N�֐�
/// </summary>
public class AnimationCallBack : MonoBehaviour
{
    bool callBack = false;

    void AnimationEndCallBack()
    {
        callBack = true;
    }

    public bool GetCallBack => callBack;
}
