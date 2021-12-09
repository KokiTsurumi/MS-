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

    public void Initialize()
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




    public void RobotDicision()
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

        robotSimpleDataUI.GetComponent<ActionRobotInterface>().SetData(name, c,b,skl,null);
    }

    public void StartButton()
    {
        Camera.main.GetComponent<CameraController>().GetCurrntAction().ActionEnd();


        //タイマー計算　
        //汚染度変化計算
        //selectRobot
        //※今は一つのRobotを利用
        RobotManager.Instance.selectedRobot = selectRobot.GetComponent<SelectRobotData>().GetOriginal();

        GameObject island = IslandManager.Instance.GetCurrentIsland();
        int removeRate = island.GetComponent<IslandBase>().CalcRemoveRate();


        string clean = RobotManager.Instance.RankTransfer(RobotManager.Instance.selectedRobot.GetComponent<RobotBase>().clean);
        string  battery = RobotManager.Instance.RankTransfer(RobotManager.Instance.selectedRobot.GetComponent<RobotBase>().battery);
        Debug.Log("ロボット性能【清掃】" + clean);
        Debug.Log("ロボット性能【駆動時間】" + battery);
        //Debug.Log("清掃時間" + time);
        island.GetComponent<IslandBase>().StartClean(1);

        //もし清掃が100％完了していたら
        //"!"アイコン表示→クリックされたらカメラ移動→海開放アニメーション
        if (island.GetComponent<IslandBase>().GetPollutionLevel() >= 100)
        {
            Debug.Log("島　清掃　完了　！！");
        }



        StartCoroutine(ActionEnd());
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

            obj.GetComponent<ActionRobotInterface>().SetData(name, c,b,skl,RobotManager.Instance.robotList[i].gameObject);
            obj.GetComponent<ActionRobotInterface>().Create();
        }
    }

    IEnumerator ActionEnd()
    {
        //数秒後にカメラを戻す
        yield return new WaitForSeconds(1.0f);

        //base.StartButton();

    }
}