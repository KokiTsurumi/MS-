using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CleaningCanvas : MonoBehaviour
{
    [SerializeField]
    GameObject robotSimpleDataUI;//キャラデータ簡易表示UI

    [SerializeField]
    GameObject robotPrefab;

    [SerializeField]
    GameObject robotListParent;


    [SerializeField]
    protected GameObject ListUI;

    [SerializeField]
    protected GameObject startButton;

    [SerializeField]
    List<GameObject> RobotList;


    [SerializeField]
    GameObject selectRobot;


    [SerializeField]

    GameObject select = null;

    [SerializeField]
    protected SelectScrollbar listScrollbar;

    virtual public void Initialize()
    {

        RobotList = RobotManager.Instance.robotList;

        CreateRobotList();

        startButton.SetActive(false);
        ListUI.SetActive(false);
    }

    public void DisplayCharaList()
    {
        if (!MouseManager.Instance.OnDoubleClickUI()) return;

        ListUI.SetActive(true);

        listScrollbar.ScrollbarPositionReset();
    }




    virtual public void RobotDicision()
    {
        SetRobotData();

        ListUI.SetActive(false);

        if (selectRobot.GetComponent<SelectRobotData>().GetSelectGameObject() != null)
        {
            startButton.SetActive(true);
        }
    }

    virtual public void SetRobotData()
    {
        GameObject setChara = selectRobot;

        //データセット
        setChara.GetComponent<SelectRobotData>().SetData(ref select);


        SimpleRobotDataDisplay();
    }

    public void RobotDataBack()
    {
        select = MouseManager.Instance.GetCurrentSelectedGameObject();
    }

    public void SimpleRobotDataDisplay()
    {
        SelectRobotData data = selectRobot.GetComponent<SelectRobotData>();
        if (data.GetSelectGameObject() == null) return;

        string name = data.GetName();
        int c = data.GetClean();
        int b = data.GetBattery();
        RobotBase.SPECIALSKILL_LIST skl = data.GetSkill();

        robotSimpleDataUI.GetComponent<ActionRobotInterface>().SetData(name, c,b,skl);
    }

    virtual public void StartButton()
    {
        Camera.main.GetComponent<CameraController>().GetCurrntAction().ActionEnd();
    }

    public void SelectCancel() { ListUI.SetActive(false); }

    public void CreateRobotList()
    {
        RobotList = RobotManager.Instance.robotList;

        //リスト生成
        for (int i = 0; i < RobotList.Count; i++)
        {
            GameObject obj = (GameObject)Instantiate(robotPrefab);
            

            RobotData data = RobotList[i].GetComponent<RobotData>();

            obj.transform.SetParent(robotListParent.transform);
            obj.name = "RobotData[" + data.name + "]";


            string name = data.name;
            int c = data.clean;
            int b = data.battery;
            RobotBase.SPECIALSKILL_LIST skl = data.specialSkill;

            obj.GetComponent<ActionRobotInterface>().SetData(name, c,b,skl);
            obj.GetComponent<ActionRobotInterface>().Create();
        }
    }
}