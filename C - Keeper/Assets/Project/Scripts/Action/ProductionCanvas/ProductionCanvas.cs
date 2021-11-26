using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProductionCanvas : SelectCanvasInterface
{
    [SerializeField]
    GameObject RobotCanvas;

    [SerializeField]
    GameObject robotPrefab;


    [SerializeField]
    CreateRobotData setRobot;



    private void Start()
    {
        RobotCanvas.SetActive(false);
    }
    public override void StartButton()
    {
        //�����l���琶�Y�ł��郍�{�b�g���v�Z
        CharacterManager.Instance.selectedCharacter[0] = selectChara[0].GetComponent<SelectCharacterDataInterface>().GetOriginal();
        CharacterManager.Instance.selectedCharacter[1] = selectChara[1].GetComponent<SelectCharacterDataInterface>().GetOriginal();



        DisplayCreateRobot();
        RobotCanvas.SetActive(true);

        //Camera.main.GetComponent<CameraController>().GetCurrntAction().ActionEnd();
    }

    void DisplayCreateRobot()
    {
        //createRobot.GetComponent<>

        //���{�b�g�𐶐�
        GameObject robot = (GameObject)Instantiate(robotPrefab);
        RobotBase data = robot.GetComponent<RobotBase>();

        setRobot.SetData(data.name, data.clean, data.battery, data.specialSkill);
    }

    public void CloseRobotCanvas()
    {
        //base.StartButton();

        //RobotCanvas.SetActive(false);

        //Destroy(this.gameObject);
        cameraController.ActionEnd();
    }
}
