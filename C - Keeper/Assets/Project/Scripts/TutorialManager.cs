using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialManager : SingletonMonoBehaviour<TutorialManager>
{

    [SerializeField]
    bool tutorialStart = false;


    [SerializeField] GameObject tutorialCanvasPrefab;
    [SerializeField] GameObject recruitCanvasPrefab;
    [SerializeField] GameObject investigationCanvasPrefab;
    [SerializeField] GameObject productionCanvasPrefab;
    [SerializeField] GameObject cleaningCanvasPrefab;


    [SerializeField]
    GameObject informationPopPrefab;

    GameObject navigatorText;
    GameObject tutorialCanvas;
    GameObject recruitCanvas;
    GameObject investigationCanvas;
    GameObject productionCanvas;
    GameObject cleaningCanvas;
    GameObject informationPop;

    public enum TutorialState
    {
        /// <summary>
        /// �`���[�g���A�����J�n
        /// </summary>
        No,
        /// <summary>
        /// �l�ޑI��
        /// </summary>
        Recruit,
        /// <summary>
        /// ������
        /// </summary>
        Investigation,
        /// <summary>
        /// �Z�l�̐��𒮂�
        /// </summary>
        Information,
        /// <summary>
        /// ���Y��
        /// </summary>
        Production,
        /// <summary>
        /// ���|��
        /// </summary>
        Cleanning,
        /// <summary>
        /// �����N�A�b�v��̐l�ޑI��   
        /// </summary>
        RankUpRecruit,
    }
    public TutorialState tutorialState = TutorialState.No;


    StepList stepList;



    delegate void UpdateFunc();
    UpdateFunc updateFunc;

    delegate void Coroutine();

    void Start()
    {

        //�Ȃ����Ⴄ������������o�O���C������
        //�`���[�g���A���J�n
        //StartCoroutine(CoroutineTimer(0.5f,TutorialStart));




        updateFunc = TutorialUpdateNull;


        //���͂�\��
        //�֐������s
        //�������̕ϐ��ɓ��ꂽ��
        //�������C���N�������g���ăX�e�b�v�i�s


    }

    void Update()
    {
        if (tutorialStart)
        {
            StartCoroutine(CoroutineTimer(0.5f, TutorialStart));
            tutorialStart = false;
        }


        updateFunc();
    }


    

    void TutorialUpdateNull()
    {
      
    }

    void TutorialStart()
    {
        //tutorialCanvas.SetActive(true);
        GameObject canvas = (GameObject)Instantiate(tutorialCanvasPrefab);
        canvas.transform.SetParent(this.transform);

        navigatorText = canvas.transform.GetChild(1).GetChild(1).gameObject;
        stepList = new StepList(navigatorText);

        stepList.AddListFunc(TutorialStart);
        stepList.AddListText("�l�ނ�I�����܂�");
        stepList.AddListFunc(RecruitStart);
        stepList.AddListFunc(RecruitEnd);
        stepList.AddListText("���͒������s���܂�");
        stepList.AddListFunc(InvestigationZoomIn);
        stepList.AddListFunc(InvestigationStart);
        stepList.AddListFunc(InvestigationEnd);

        stepList.AddListText("�����@�����@���@���@�Z�l�@���@�����Ă݂�");
        stepList.AddListFunc(InformationStart);
        stepList.AddListFunc(Information);
        stepList.AddListFunc(InformationEnd);
        stepList.AddListText("���͐��Y���s���܂��傤w");
        stepList.AddListFunc(ProductionPopOut);
        stepList.AddListFunc(ProductionStart);
        stepList.AddListFunc(ProductionEnd);
        stepList.AddListText("���|���s���܂�");
        stepList.AddListFunc(CleaningPopOut);
        stepList.AddListFunc(CleaningStart);
        stepList.AddListFunc(CleaningEnd);
        stepList.AddListFunc(RankUp);
        stepList.AddListText("�m���x�����N�オ������\n����");
        stepList.AddListText("����ɐl�ޑI��");
        stepList.AddListFunc(RankUpRecruitStart);
        stepList.AddListFunc(RankUpRecruitEnd);
        

        //EventTrigger�Z�b�g
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventDate) => NextStep());
        canvas.transform.GetChild(1).GetComponent<EventTrigger>().triggers.Add(entry);

        tutorialCanvas = canvas;

        stepList.Next();
        stepList.Step();

        //�ŏ��ɑI������l�ނ𐶐�
        //CharacterManager.Instance.CreateCandidateCharacter();


        
    }

    void RecruitStart()
    {
        tutorialCanvas.SetActive(false);

        CharacterManager.Instance.CreateCandidateCharacter();
        tutorialState = TutorialState.Recruit;

        recruitCanvas = Instantiate(recruitCanvasPrefab);
        recruitCanvas.GetComponent<RecruitCanvas>();
    }

    void RecruitEnd()
    {
        tutorialCanvas.SetActive(true);
        stepList.Next();
        stepList.Step();
    }

    void InvestigationZoomIn()
    {
        tutorialCanvas.SetActive(false);
        tutorialState = TutorialState.Investigation;
        Camera.main.GetComponent<CameraController>().ZoomIn();

        //stepList.Next();
    }

    void InvestigationStart()
    {
        investigationCanvas = Instantiate(investigationCanvasPrefab);
        investigationCanvas.GetComponent<InvestigationCanvas>().Initialize();
        updateFunc = InvestigationUpdate;
    }

    void InvestigationUpdate()
    {

        GameObject island = IslandManager.Instance.GetCurrentIsland();

        if (island.GetComponent<IslandBase>().state == IslandBase.STATE_ISLAND.STATE_INVESTIGATED)
        {
            updateFunc = TutorialUpdateNull;


            stepList.Next();
            StartCoroutine(CoroutineTimer(0.5f, stepList.Step));
        }
    }

    void InvestigationEnd()
    {
        stepList.Next();
        stepList.Step();
        tutorialCanvas.SetActive(true);

    }

    void InformationStart()
    {
        tutorialCanvas.SetActive(false);
        tutorialState = TutorialState.Information;
        Camera.main.GetComponent<CameraController>().ZoomIn();
    }

    void Information()
    {
        //IslandManager.Instance.GetCurrentIsland().GetComponent<IslandBase>().icon.SetActive(true);

        informationPop = Instantiate(informationPopPrefab);
        
    }

    void InformationEnd()
    {
        //IslandManager.Instance.GetCurrentIsland().GetComponent<IslandBase>().icon.SetActive(true);
        tutorialCanvas.SetActive(true);
        NextStep();
    }

    void ProductionPopOut()
    {
        tutorialCanvas.SetActive(false);
        tutorialState = TutorialState.Production;
        Camera.main.GetComponent<CameraController>().ZoomIn();
    }

    void ProductionStart()
    {
        productionCanvas = Instantiate(productionCanvasPrefab);
        productionCanvas.GetComponent<ProductionCanvas>().Initialize();
    }

    void ProductionEnd()
    {
        stepList.Next();
        stepList.Step();
        tutorialCanvas.SetActive(true);

    }

    void CleaningPopOut()
    {
        tutorialState = TutorialState.Cleanning;
        tutorialCanvas.SetActive(false);
        tutorialState = TutorialState.Cleanning;
        Camera.main.GetComponent<CameraController>().ZoomIn();
    }

    void CleaningStart()
    {
        cleaningCanvas = Instantiate(cleaningCanvasPrefab);
        cleaningCanvas.GetComponent<CleaningCanvas>().Initialize();
    }
    
    void CleaningEnd()
    {
        stepList.Next();
        stepList.Step();
        tutorialCanvas.SetActive(true);
    }

    void RankUp()
    {
        //�m���x�����N�PUp
        WorldManager.Instance.IncreasePopularityRank();
        Name_Value.Instance.PlusCleaningCount();
        Name_Value.Instance.PlusPlacementCountt();
        Name_Value.Instance.PlusProductionCount();
        Name_Value.Instance.PlusResearchCount();
        Name_Value.Instance.RankConfirm();

        Camera.main.GetComponent<CameraController>().ZoomOut();
        NextStep();
        
    }

    void RankUpRecruitStart()
    {
        tutorialCanvas.SetActive(false);

        CharacterManager.Instance.CreateCandidateCharacter();
        tutorialState = TutorialState.RankUpRecruit;

        recruitCanvas = Instantiate(recruitCanvasPrefab);
        recruitCanvas.GetComponent<RecruitCanvas>();
    }

    void RankUpRecruitEnd()
    {
        tutorialCanvas.SetActive(true);
        stepList.Next();
        stepList.Step();
    }




    IEnumerator CoroutineTimer(float time,Coroutine coroutine)
    {
        yield return new WaitForSeconds(time);
        coroutine();
    }

    public void NextStep()
    {
        stepList.Next();
        stepList.Step();
    }

}

class StepList
{
    List<StepInterface> stepList;

    GameObject text;

    int number = 0;

    public StepList(GameObject text)
    {
        stepList = new List<StepInterface>();
        this.text = text;
    }

    public void AddListFunc(StepFunc.Func func)
    {
        stepList.Add(new StepFunc(func));
    }

    public void AddListText(string str)
    {
        stepList.Add(new StepText(text, str));
    }

    public void Step()
    {
        stepList[number].Step();
        //Debug.Log(number);
    }

    public void Next()
    {
        number++;
    }
}



class StepText : StepInterface
{
    GameObject text;
    string str;

    public StepText(GameObject text,string str)
    {
        this.text = text;
        this.str = str;
    }

    override public void Step()
    {
        //�\��
        text.GetComponent<Text>().text = str;
        text.GetComponent<TextFader>().enabled = true;
    }
}

class StepFunc : StepInterface
{
    public delegate void Func();
    Func func;

    public StepFunc(Func func)
    {
        this.func = func;
    }

    override public void Step()
    {
        func();
    }
}



class StepInterface
{
    public StepInterface(){}
    virtual public void Step(){}
}
