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
        //base.StartButton();

        //�����l���琶�Y�ł��郍�{�b�g���v�Z

        //DisplayCreateRobot();
        RobotCanvas.SetActive(true);
    }

    void DisplayCreateRobot()
    {

    }

    public void CloseRobotCanvas()
    {
        base.StartButton();
    }
}