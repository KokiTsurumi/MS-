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
        int removeRate = island.GetComponent<IslandBase>().CalcRemoveRate(true);

        cleaningCanvas.SetActive(false);
        selectCanvas.SetActive(false);

        if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Cleanning)
        {

            island.GetComponent<IslandBase>().StartClean(3.0f,TutorialCleaningEnd);
           
        }
        else
        {
            float time = RobotManager.Instance.CalcCleanTime();

            island.GetComponent<IslandBase>().StartClean(time,null);

            //もし清掃が100％完了していたら
            //"!"アイコン表示→クリックされたらカメラ移動→海開放アニメーション
            if (island.GetComponent<IslandBase>().GetPollutionLevel() >= 100)
            {
                Debug.Log("島　清掃　完了　！！");
            }

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

    public void TutorialCleaningEnd()
    {
        Debug.Log("cleaning end ");
        StartCoroutine(CleaningEnd());
        
    }

    IEnumerator CleaningEnd()
    {
        yield return new WaitForSeconds(1.0f);
        TutorialManager.Instance.NextStep();
        Destroy(this.gameObject);

    }
}