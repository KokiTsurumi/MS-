using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionRobotInterface : MonoBehaviour
{
    

    CleaningCanvas canvas;

  

    public bool isSelected { get; set; } = false;

    

    public void onClick()
    {
        canvas.CharaDataBack();
    }

    public void Create()
    {
        GameObject root = transform.root.gameObject;
        canvas = root.transform.GetChild(0).GetComponent<CleaningCanvas>();
    }

}