using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActionButtonInterface : MonoBehaviour
{
    protected CameraController cameraController;
    protected void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>(); 
    }

    virtual public void ActionStart() { }
    virtual public void ActionEnd() { }
    virtual public void DisplayUI() { }
}
