using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�j���[�V�����I�����R�[���o�b�N�֐�
/// </summary>
public class AnimationCallBack : MonoBehaviour
{
    [SerializeField]
    bool callBack = false;

    [SerializeField] AudioSource sound;

    void AnimationEndCallBack()
    {
        callBack = true;

        if (sound != null)
            sound.Play();
    }

    public bool GetCallBack => callBack;

    public void AnimationStart()
    {
        callBack = false;
    }
}
