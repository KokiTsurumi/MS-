using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CleaningCanvas : SelectCanvasInterface
{
    public override void Initialize()
    {
        RobotList = RobotManager.Instance.robotList;

        CreateRobitList();

        startButton.SetActive(false);
        ListUI.SetActive(false);
    }

    void CreateRobitList()
    {

    }

    public void DisplayRobotList()
    {
        if (!MouseManager.Instance.OnDoubleClickUI()) return;

        ListUI.SetActive(true);

        listScrollbar.ScrollbarPositionReset();
    }
}
