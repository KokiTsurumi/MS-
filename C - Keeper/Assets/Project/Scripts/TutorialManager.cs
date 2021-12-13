using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 　チュートリアル　シングルトン　クラス
/// </summary>
public class TutorialManager : SingletonMonoBehaviour<TutorialManager>
{

    [SerializeField] bool tutorialStart = false;
    bool startBool = true;

    [SerializeField] GameObject tutorialCanvasPrefab;
    [SerializeField] GameObject recruitCanvasPrefab;
    [SerializeField] GameObject investigationCanvasPrefab;
    [SerializeField] GameObject productionCanvasPrefab;
    [SerializeField] GameObject cleaningCanvasPrefab;

    [SerializeField] GameObject informationPopPrefab;


    GameObject navigatorText;
    GameObject tutorialCanvas;
    GameObject recruitCanvas;
    GameObject investigationCanvas;
    GameObject productionCanvas;
    GameObject cleaningCanvas;
    GameObject informationPop;

    //bool textFadeSkip = false;

    public enum TutorialState
    {
        /// <summary>
        /// チュートリアル未開始
        /// </summary>
        No,
        /// <summary>
        /// 人材選択
        /// </summary>
        Recruit,
        /// <summary>
        /// 調査中
        /// </summary>
        Investigation,
        /// <summary>
        /// 住人の声を聴く
        /// </summary>
        Information,
        /// <summary>
        /// 生産中
        /// </summary>
        Production,
        /// <summary>
        /// 清掃中
        /// </summary>
        Cleanning,
        /// <summary>
        /// ランクアップ後の人材選択   
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
        //なぜか違う文字がちらつくバグも修正する

        updateFunc = TutorialUpdateNull;

        
    }

    void Update()
    {
       

        if(startBool)
        {
            if (!tutorialStart)
            {
                Name_Value.Instance.PlusCleaningCount();
                Name_Value.Instance.PlusPlacementCountt();
                Name_Value.Instance.PlusProductionCount();
                Name_Value.Instance.PlusResearchCount();
                Name_Value.Instance.RankConfirm();
                RankUpUI.Instance.RankUpCheck();
            }
            else
            {
                TutorialStart();
            }

            startBool = false;
        }


        if(tutorialCanvas != null)
        {
            if (tutorialCanvas.transform.GetChild(1).GetChild(1).GetComponent<TextFader>().enabled == true)
                tutorialCanvas.transform.GetChild(1).GetChild(2).GetComponent<Image>().enabled = false;
            else
                tutorialCanvas.transform.GetChild(1).GetChild(2).GetComponent<Image>().enabled = true;
        }
       


        updateFunc();
    }


    
    //DummyFunction
    void TutorialUpdateNull()
    {   
    }

    void TutorialStart()
    {
        GameObject canvas = (GameObject)Instantiate(tutorialCanvasPrefab);
        canvas.transform.SetParent(this.transform);

        navigatorText = canvas.transform.GetChild(1).GetChild(1).gameObject;
        stepList = new StepList(navigatorText);

        stepList.AddListFunc(TutorialStart);
        stepList.AddListText("人材を選択します");
        stepList.AddListFunc(RecruitStart);
        stepList.AddListFunc(RecruitEnd);
        stepList.AddListText("次は調査を行います");
        stepList.AddListFunc(InvestigationZoomIn);
        stepList.AddListFunc(InvestigationStart);
        stepList.AddListFunc(InvestigationEnd);
        stepList.AddListText("調査　完了　次　島　住人　声　聞いてみる");
        stepList.AddListFunc(InformationStart);
        stepList.AddListFunc(Information);
        stepList.AddListFunc(InformationEnd);
        stepList.AddListText("次は生産を行いましょうw                    ");
        stepList.AddListFunc(ProductionPopOut);
        stepList.AddListFunc(ProductionStart);
        stepList.AddListFunc(ProductionEnd);
        stepList.AddListText("清掃を行います          ");
        stepList.AddListFunc(CleaningPopOut);
        stepList.AddListFunc(CleaningStart);
        stepList.AddListFunc(CleaningEnd);
        stepList.AddListFunc(RankUp);
        stepList.AddListText("拠点となる島がきれいになったよ\n知名度ランク上るよ\nｲｪｰｲ");
        stepList.AddListText("ランクが上がったのでさらに人材を雇うことができます      ");
        stepList.AddListFunc(RankUpRecruitStart);
        stepList.AddListFunc(RankUpRecruitEnd);
        stepList.AddListText("以上でチュートリアルを終了します");
        stepList.AddListText("周囲にある4つの島を清掃してきれいな海にしましょう");
        stepList.AddListFunc(TutorialEnd);

        //EventTriggerセット
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventDate) => TextNextStep());
        canvas.transform.GetChild(1).GetComponent<EventTrigger>().triggers.Add(entry);

        tutorialCanvas = canvas;

        stepList.Next();
        stepList.Step();

        //最初に選択する人材を生成
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
        informationPop.GetComponent<InformationPop>().Create("チュートリアル", 0);
        IslandManager.Instance.GetCurrentIsland().GetComponent<IslandBase>().icon.GetComponent<Canvas>().enabled = false;
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
        //知名度ランク１Upのための調節
        Name_Value.Instance.PlusCleaningCount();
        Name_Value.Instance.PlusPlacementCountt();
        //Name_Value.Instance.PlusProductionCount();
        Name_Value.Instance.PlusResearchCount();
        Name_Value.Instance.RankConfirm();


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

    void TutorialEnd()
    {
        //Destroy(this.gameObject);//これでもいい
        tutorialCanvas.SetActive(false);
        tutorialState = TutorialState.No;
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

    public void TextNextStep()
    {

        //フェーディング中
        if (tutorialCanvas.transform.GetChild(1).GetChild(1).GetComponent<TextFader>().enabled == true)
        {
            tutorialCanvas.transform.GetChild(1).GetChild(1).GetComponent<TextFader>().enabled = false;

        }
        //フェーディング済
        else
        {
            stepList.Next();
            stepList.Step();

        }
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
        //表示
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
