using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 清掃　UI　クラス
/// </summary>
public class CleaningCanvas : MonoBehaviour
{
    [SerializeField]GameObject robotSimpleDataUI;//キャラデータ簡易表示UI

    [SerializeField] GameObject robotPrefab;

    [SerializeField] GameObject robotListParent;

    [SerializeField] GameObject ListUI;

    [SerializeField] GameObject startButton;

    [SerializeField] List<GameObject> RobotList;

    [SerializeField] GameObject selectRobot;

    [SerializeField] SelectScrollbar listScrollbar;

    [SerializeField] GameObject cleaningCanvas;
    [SerializeField] GameObject selectCanvas;

    GameObject island;
    GameObject select = null;

    public void Initialize()
    {

        RobotList = new List<GameObject>();

        CreateRobotList();

        startButton.SetActive(false);
        ListUI.SetActive(false);

        island = IslandManager.Instance.GetCurrentIsland();
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
        setChara.GetComponent<SelectRobotData>().SetData(select);


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

        robotSimpleDataUI.GetComponent<ActionRobotInterface>().SetData(data);
    }

    public void StartButton()
    {


        RobotManager.Instance.selectedRobot = selectRobot.GetComponent<SelectRobotData>().originalGameObject;


        cleaningCanvas.SetActive(false);
        selectCanvas.SetActive(false);

        island.GetComponent<IslandBase>().timer.GetComponent<Canvas>().enabled = true;


        if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Cleanning)
        {
            //Debug.Log("チュートリアル清掃開始");
            TutorialManager.Instance.CleaningTimerSet(TutorialCleaning);
            
           
        }
        else
        {
            float time = RobotManager.Instance.CalcCleanTime();

            island.GetComponent<IslandBase>().StartClean(time,CleaningEnd);

           
            StartCoroutine(ActionEnd());

        }


    }

    public void SelectCancel() { ListUI.SetActive(false); }

    public void CreateRobotList()
    {
       
        foreach(GameObject original in RobotManager.Instance.robotList)
        {
            if (original.transform.position.x == 1) continue;

            GameObject obj = (GameObject)Instantiate(robotPrefab);


            //RobotData data = RobotList[i].GetComponent<RobotData>();

            obj.transform.SetParent(robotListParent.transform, false);
            obj.name = "RobotData[" + original.GetComponent<RobotData>().name + "]";

            obj.GetComponent<ActionRobotInterface>().Create(original);

            RobotList.Add(obj);
        }
    }

    IEnumerator ActionEnd()
    {
        //Camera.main.GetComponent<CameraController>().GetCurrntAction().ActionEnd();
        Camera.main.GetComponent<CameraController>().ActionEnd();

        //数秒後にカメラを戻す
        yield return new WaitForSeconds(1.0f);

        //base.StartButton();

    }

    public void TutorialCleaningStart()
    {
        int level = island.GetComponent<IslandBase>().CalcRemoveRate(true);
        //Debug.Log("レベル" + level);
        island.GetComponent<IslandBase>().RemovePollution(level);
        Camera.main.GetComponent<CameraController>().ZoomOut();
        island.transform.GetChild(0).GetComponent<SeaDizolve>().DissolveStart();
        //StartCoroutine(TutorialCleaningEnd());

    }

    IEnumerator TutorialCleaningEnd()
    {
        Camera.main.GetComponent<CameraController>().ZoomOut();
        yield return new WaitForSeconds(0.3f);
        island.transform.GetChild(0).GetComponent<SeaDizolve>().DissolveStart();
        yield return new WaitForSeconds(3.0f);

        //TutorialManager.Instance.NextStep();
        Destroy(this.gameObject);

    }

    public void CleaningEnd()
    {
        int level = island.GetComponent<IslandBase>().CalcRemoveRate(false);
        //Debug.Log("レベル" + level);
        island.GetComponent<IslandBase>().RemovePollution(level);
        Name_Value.Instance.PlusCleaningCount();
        //Name_Value.Instance.RankConfirm();
        //RankUpUI.Instance.RankUpCheck();
        //if (island.GetComponent<IslandBase>().GetPollutionLevel() <= 0)
        //{
        //    Debug.Log("島　清掃　完了　！！");
        //    island.transform.GetChild(0).GetComponent<SeaDizolve>().start = true;
        //}

    }

    public void TutorialCleaning()
    {
        island.GetComponent<IslandBase>().StartClean(2.0f, TutorialCleaningStart);
    }
}