using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������@�u�I�v�A�C�R���@�N���X
/// </summary>
public class ExMarkIcon : MonoBehaviour
{
    [SerializeField]
    GameObject information_Pop;

    public void DisplayInformaionPop()
    {
        information_Pop.SetActive(true);
    }
}
