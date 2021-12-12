using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 調査完了　「！」アイコン　クラス
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
