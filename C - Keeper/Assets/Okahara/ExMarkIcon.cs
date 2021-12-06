using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExMarkIcon : MonoBehaviour
{
    [SerializeField]
    GameObject information_Pop;

    public void DisplayInformaionPop()
    {
        information_Pop.SetActive(true);
    }
}
