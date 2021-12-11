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


    GameObject select = null;

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


        RobotManager.Instance.selectedRobot = selectRobot.GetComponent<SelectRobotData>().GetOriginal();

        GameObject island = IslandManager.Instance.GetCurrentIsland();
        

        cleaningCanvas.SetActive(false);
        selectCanvas.SetActive(false);

        island.GetComponent<IslandBase>().timer.GetComponent<Canvas>().enabled = true;


        if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Cleanning)
        {
            Debug.Log("チュートリアル清掃開始");
            island.GetComponent<IslandBase>().StartClean(3.0f,TutorialCleaning);
           
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
        Camera.main.GetComponent<CameraController>().GetCurrntAction().ActionEnd();

        //数秒後にカメラを戻す
        yield return new WaitForSeconds(1.0f);

        //base.StartButton();

    }

    public void TutorialCleaning()
    {
        GameObject island = IslandManager.Instance.GetCurrentIsland();
        int level = island.GetComponent<IslandBase>().CalcRemoveRate(true);
        Debug.Log("レベル" + level);
        island.GetComponent<IslandBase>().RemovePollution(level);

        StartCoroutine(TutorialCleaningEnd());
        
    }

    IEnumerator TutorialCleaningEnd()
    {
        GameObject island = IslandManager.Instance.GetCurrentIsland();
        Camera.main.GetComponent<CameraController>().ZoomOut();
        yield return new WaitForSeconds(0.3f);
        island.transform.GetChild(0).GetComponent<SeaDizolve>().start = true;
        yield return new WaitForSeconds(3.0f);

        TutorialManager.Instance.NextStep();
        Destroy(this.gameObject);

    }

    public void CleaningEnd()
    {
        GameObject island = IslandManager.Instance.GetCurrentIsland();
        int level = island.GetComponent<IslandBase>().CalcRemoveRate(false);
        Debug.Log("レベル" + level);
        island.GetComponent<IslandBase>().RemovePollution(level);
        //if (island.GetComponent<IslandBase>().GetPollutionLevel() <= 0)
        //{
        //    Debug.Log("島　清掃　完了　！！");
        //    island.transform.GetChild(0).GetComponent<SeaDizolve>().start = true;
        //}

    }
}