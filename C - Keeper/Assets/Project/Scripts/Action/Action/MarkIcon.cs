using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������@�u�I�v�A�C�R���@�N���X
/// </summary>
public class MarkIcon : MonoBehaviour
{
    bool watched = false;

    //bool toggle = false;

    public bool GetWatched => watched;
    public void SetWatched()
    {
        watched = true;
        this.gameObject.SetActive(false);
    }


    //private void Update()
    //{
    //    //if (toggle) return;

    //    //if(watched)
    //    //{
    //    //    this.gameObject.SetActive(false);
    //    //    toggle = false;
    //    //}
    //}
}
