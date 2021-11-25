using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProductionCanvas : SelectCanvasInterface
{
    [SerializeField]
    GameObject RobotCanvas;

    private void Start()
    {
        RobotCanvas.SetActive(false);
    }
    public override void StartButton()
    {
        //研究値から生産できるロボットを計算

        //DisplayCreateRobot();
        RobotCanvas.SetActive(true);

        //Camera.main.GetComponent<CameraController>().GetCurrntAction().ActionEnd();
    }

    void DisplayCreateRobot()
    {

    }

    public void CloseRobotCanvas()
    {
        //base.StartButton();

        //RobotCanvas.SetActive(false);

        //Destroy(this.gameObject);
        cameraController.ActionEnd();
    }
}
