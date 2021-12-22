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

    //[SerializeField] GameObject cursor;

    [SerializeField] Sprite smileFace;
    [SerializeField] Sprite sadFace;

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
        InvestigationStart,
        Investigation,
        /// <summary>
        /// 住人の声を聴く
        /// </summary>
        InformationStart,
        Information,
        /// <summary>
        /// 生産中
        /// </summary>
        ProductionStart,
        Production,
        ProductionRobotCreate,
        /// <summary>
        /// 清掃中
        /// </summary>
        CleanningStart,
        Cleanning,
        /// <summary>
        /// ランクアップ後の人材選択   
        /// </summary>
        RankUpRecruit,
        /// <summary>
        /// メニュー
        /// </summary>
        Menu,
    }
    public TutorialState tutorialState = TutorialState.No;


    StepList stepList;


    public delegate void DelegateFunc();
    public DelegateFunc delegateFunc;

    delegate void Coroutine();

    void Start()
    {
        //cursor.SetActive(false);
        TutorialCursor.Instance.SetActive(false);
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
            if (tutorialCanvas.transform.GetChild(2).GetChild(1).GetComponent<TextFader>().enabled == true)
            {
                tutorialCanvas.transform.GetChild(2).GetChild(2).GetComponent<Image>().enabled = false;
            }
            else
            {
                tutorialCanvas.transform.GetChild(2).GetChild(2).GetComponent<Image>().enabled = true;
                tutorialCanvas.transform.GetChild(2).GetChild(1).GetComponent<AudioSource>().Stop();
            }

        }
       
    }


    
    //DummyFunction
    void TutorialUpdateNull()
    {   
    }

    void TutorialStart()
    {
        GameObject canvas = (GameObject)Instantiate(tutorialCanvasPrefab);
        canvas.transform.SetParent(this.transform);

        navigatorText = canvas.transform.GetChild(2).GetChild(1).gameObject;
        stepList = new StepList(navigatorText);

        stepList.AddListFunc(TutorialStart);
        stepList.AddListText("はじめまして！これからこの組織の秘書を努めます(秘書子)です！");
        stepList.AddListText("この組織は、汚れてしまった海域を綺麗にし、居なくなった魚たちを呼び戻す組織です！");
        stepList.AddListText("それでは最初にこの組織で働いてもらう人員を雇いましょう！");
        stepList.AddListFunc(RecruitStart);
        stepList.AddListFunc(RecruitEnd);
        stepList.AddListText("中々いい人材を選びましたね！");
        stepList.AddListText("それでは早速この2人に仕事を割り振りましょう！");
        stepList.AddListText("まず最初に島の状況を把握しましょう！");
        stepList.AddListText("島をクリックしてください！");
        stepList.AddListFunc(InvestigationZoomIn);
        stepList.AddListFunc(InvestigationTextStart);
        stepList.AddListText("次にコマンド調査を選択してください");
        stepList.AddListFunc(InvestigationTextEnd);
        stepList.AddListFunc(InvestigationStart);
        stepList.AddListText("空きをクリックすると人材が配置できますよ！");
        stepList.AddListFunc(InvestigationSelectText);
        stepList.AddListText("人材配置完了です！");
        stepList.AddListText("調査や清掃を行うと時間がかかるので、完了まで少々お待ちくださいね！");
        stepList.AddListFunc(InvestigationTimerStart);
        stepList.AddListFunc(InvestigationEnd);
        stepList.AddListText("完了しました！\n調査を行うと、その島はどれだけ汚染されているのかと住民の声を聞く事が出来ます！");
        stepList.AddListText("島をクリックして、情報を選択しましょう！");
        stepList.AddListFunc(InformationStart);
        stepList.AddListFunc(Information);
        stepList.AddListText("この島ではゴミ問題に悩まされているようですね・・・");
        stepList.AddListFunc(InformationText);
        stepList.AddListFunc(InformationEnd);
        stepList.AddListText("それではロボットを生産してそれを使って清掃しましょう！");
        stepList.AddListText("先程のコマンドから生産をクリックしてください！");
        stepList.AddListFunc(ProductionPopOut);
        stepList.AddListFunc(ProductionStart);
        stepList.AddListText("空いている場所をクリックして人材を配置してください！");
        stepList.AddListFunc(ProductionSelectText);
        stepList.AddListText("生産の配置完了です！ロボットが生産されるまで少々お待ちください！");
        stepList.AddListFunc(ProductionTimerStart);
        stepList.AddListFunc(ProductionEnd);
        stepList.AddListText("ロボットが完成しました！島をクリックしてロボットを見てみましょう");
        stepList.AddListFunc(ProductionTextEnd);
        stepList.AddListFunc(CleaningTextStart);
        stepList.AddListText("それではロボットを配置して清掃を開始しましょう！、まずは清掃のコマンドを選択してください！");
        stepList.AddListFunc(CleaningPopOut);
        stepList.AddListFunc(CleaningStart);
        stepList.AddListText("清掃開始です、しばらくお待ちください！");
        stepList.AddListFunc(CleaningTimerStart);
        stepList.AddListFunc(CleaningEnd);
        stepList.AddListFunc(SeaDissolveStart);
        stepList.AddListFunc(SeaDissolveEnd);
        stepList.AddListText("清掃が終わりました！、汚染度は、情報をクリックした後に島をクリックすればわかるようになっています！");
        stepList.AddListText("そしてこの島の汚染度が0%になり、きれいな海に戻りました！");
        stepList.AddListFunc(RankUp);
        stepList.AddListText("知名度ランクが上がりました！");
        stepList.AddListText("知名度が上がったことで、組織に入りたいと言っている方が来ています！誰を雇いますか？");
        stepList.AddListFunc(RankUpRecruitStart);
        stepList.AddListFunc(RankUpRecruitEnd);
        stepList.AddListText("いいと思います！これで準備は完了しました！");
        stepList.AddListText("このゲームの各機能の説明は、メニューの説明に入っています！");
        stepList.AddListFunc(MenuStart);
        stepList.AddListFunc(MenuEnd);
        stepList.AddListText("この海域の汚染を全て取り除けば、生態系は元に戻ります！それでは、頑張って下さい！");
        stepList.AddListFunc(TutorialEnd);

        //EventTriggerセット
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventDate) => TextNextStep());
        canvas.transform.GetChild(2).GetComponent<EventTrigger>().triggers.Add(entry);

        tutorialCanvas = canvas;

        stepList.Next();
        stepList.Step();

        MenuCanvas.Instance.menuButtonEnabled = false;
        MenuCanvas.Instance.tutorialExplanation = false;
        WorldManager.Instance.TimeStop(true);

    }

    void RecruitStart()
    {
        tutorialCanvas.SetActive(false);

        CharacterManager.Instance.CreateCandidateCharacter();
        tutorialState = TutorialState.Recruit;

        recruitCanvas = Instantiate(recruitCanvasPrefab);
        recruitCanvas.GetComponent<RecruitCanvas>();

        //cursor.SetActive(true);
        //cursor.GetComponent<TutorialCursor>().SetPosition(TutorialCursor.CursorPositionList.characterSelectSloteft);
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
        tutorialState = TutorialState.InvestigationStart;

        TutorialCursor.Instance.SetActive(true);
        TutorialCursor.Instance.SetPosition(TutorialCursor.CursorPositionList.centerIsland);

    }


    void InvestigationTextStart()
    {
        Camera.main.GetComponent<CameraController>().ActionButtonRepop();
        StartCoroutine(CoroutineTimer(1.0f, InvestigationText));
        
    }

    void InvestigationText()
    {
        tutorialState = TutorialState.Investigation;
        tutorialCanvas.SetActive(true);
        NextStep();
    }

    void InvestigationTextEnd()
    {
        tutorialCanvas.SetActive(false);

        TutorialCursor.Instance.SetActive(true);
        TutorialCursor.Instance.SetPosition(TutorialCursor.CursorPositionList.investigationUI);
    }

    void InvestigationStart()
    {
        TutorialCursor.Instance.SetActive(false);

        tutorialCanvas.SetActive(true);
        tutorialState = TutorialState.Investigation;
        investigationCanvas = Instantiate(investigationCanvasPrefab);
        investigationCanvas.GetComponent<InvestigationCanvas>().Initialize();
        NextStep();
    }

    void InvestigationSelectText()
    {
        tutorialCanvas.SetActive(false);

        TutorialCursor.Instance.SetActive(true);
        TutorialCursor.Instance.SetPosition(TutorialCursor.CursorPositionList.characterSelectSlotelft);
    }
   

    void InvestigationEnd()
    {
        Destroy(investigationCanvas);
        stepList.Next();
        stepList.Step();
        tutorialCanvas.SetActive(true);
        Camera.main.GetComponent<CameraController>().ZoomOut();
    }


    public void InvestigationTimerSet(DelegateFunc func)
    {
        delegateFunc = func;
        tutorialCanvas.SetActive(true);
        NextStep();
    }

    void InvestigationTimerStart()
    {
        tutorialCanvas.SetActive(false);
        delegateFunc();
        
    }

    void InformationStart()
    {
        tutorialCanvas.SetActive(false);
        tutorialState = TutorialState.Information;
        Camera.main.GetComponent<CameraController>().ZoomIn();

        tutorialState = TutorialState.InformationStart;

        TutorialCursor.Instance.SetPosition(TutorialCursor.CursorPositionList.informationUI);


        //困っている表情
        tutorialCanvas.gameObject.transform.GetChild(1).GetChild(3).GetComponent<Image>().sprite = sadFace;
    }


    void Information()
    {
        //tutorialState = TutorialState.Information;
        informationPop = Instantiate(informationPopPrefab);
        informationPop.GetComponent<InformationPop>().Create(IslandManager.Instance.GetCurrentIsland());
        IslandManager.Instance.GetCurrentIsland().GetComponent<IslandBase>().icon.GetComponent<Canvas>().enabled = false;
        NextStep();
        tutorialCanvas.SetActive(true);

        TutorialCursor.Instance.SetActive(false);
    }

    void InformationText()
    {
        tutorialCanvas.SetActive(false);
    }

    void InformationEnd()
    {
        Camera.main.GetComponent<CameraController>().ZoomOut();
        StartCoroutine(CoroutineTimer(0.8f, InformationEndTimer));

        //笑っている表情に戻す
        tutorialCanvas.gameObject.transform.GetChild(1).GetChild(3).GetComponent<Image>().sprite = smileFace;
    }

    void InformationEndTimer()
    {
        tutorialCanvas.SetActive(true);
        NextStep();
    }

    void ProductionPopOut()
    {
        tutorialCanvas.SetActive(false);
        tutorialState = TutorialState.ProductionStart;
        Camera.main.GetComponent<CameraController>().ZoomIn();

        TutorialCursor.Instance.SetPosition(TutorialCursor.CursorPositionList.productionUI);
    }

    void ProductionStart()
    {
        TutorialCursor.Instance.SetActive(false);


        productionCanvas = Instantiate(productionCanvasPrefab);
        productionCanvas.GetComponent<ProductionCanvas>().Initialize();
        tutorialCanvas.SetActive(true);
        NextStep();
    }

    void ProductionSelectText()
    {
        tutorialCanvas.SetActive(false);

        TutorialCursor.Instance.SetActive(true);
        TutorialCursor.Instance.SetPosition(TutorialCursor.CursorPositionList.characterSelectSlotelft);
    }

    public void ProductionTimerSet(DelegateFunc func)
    {
        delegateFunc = func;
        tutorialCanvas.SetActive(true);
        NextStep();
    }

    void ProductionTimerStart()
    {
        tutorialCanvas.SetActive(false);
        delegateFunc();
    }

    void ProductionEnd()
    {
        //NextStep();
        tutorialCanvas.SetActive(true);
        NextStep();
    }

    void ProductionTextEnd()
    {
        tutorialCanvas.SetActive(false);

        TutorialCursor.Instance.SetActive(true);
        TutorialCursor.Instance.SetPosition(TutorialCursor.CursorPositionList.productionCenterIsland);
    }

    void CleaningTextStart()
    {
        NextStep();
        tutorialCanvas.SetActive(true);
        tutorialState = TutorialState.Cleanning;
    }


    void CleaningPopOut()
    {
        tutorialCanvas.SetActive(false);
        Camera.main.GetComponent<CameraController>().ZoomIn();

        tutorialState = TutorialState.CleanningStart;
        TutorialCursor.Instance.SetPosition(TutorialCursor.CursorPositionList.cleaningUI);
    }

    void CleaningStart()
    {
        cleaningCanvas = Instantiate(cleaningCanvasPrefab);
        cleaningCanvas.GetComponent<CleaningCanvas>().Initialize();

        TutorialCursor.Instance.SetPosition(TutorialCursor.CursorPositionList.robotSelectSlotleft);
    }

    public void CleaningTimerSet(DelegateFunc func)
    {
        delegateFunc = func;
        tutorialCanvas.SetActive(true);
        NextStep();
    }

    void CleaningTimerStart()
    {
        tutorialCanvas.SetActive(false);
        delegateFunc();


    }

    void CleaningEnd()
    {
        stepList.Next();
        stepList.Step();
    }

    void SeaDissolveStart()
    {
        GameObject island = IslandManager.Instance.GetCurrentIsland();
        island.transform.GetChild(0).GetComponent<SeaDizolve>().DissolveStart();

    }

    void SeaDissolveEnd()
    {
        NextStep();
        tutorialCanvas.SetActive(true);
    }

    void RankUp()
    {
        //知名度ランク１Upのための調節
        Name_Value.Instance.PlusCleaningCount();
        Name_Value.Instance.PlusPlacementCountt();
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

    void MenuStart()
    {
        tutorialCanvas.SetActive(false);
        //NextStep();

        TutorialCursor.Instance.SetActive(true);
        TutorialCursor.Instance.SetPosition(TutorialCursor.CursorPositionList.menuButton);
        TutorialCursor.Instance.SetScaleChange(-1);

        MenuCanvas.Instance.menuButtonEnabled = true;
        MenuCanvas.Instance.tutorialExplanation = true;

        tutorialState = TutorialState.Menu;

    }

    void MenuEnd()
    {
        TutorialCursor.Instance.SetActive(false);
        NextStep();
        tutorialCanvas.SetActive(true);
        //NextStep();
    }

    void TutorialEnd()
    {
        WorldManager.Instance.TimeStop(false);

        //Destroy(this.gameObject);//これでもいい
        tutorialCanvas.SetActive(false);
        tutorialState = TutorialState.No;
        Camera.main.GetComponent<CameraController>().ZoomOut();
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
        if (tutorialCanvas.transform.GetChild(2).GetChild(1).GetComponent<TextFader>().enabled == true)
        {
            tutorialCanvas.transform.GetChild(2).GetChild(1).GetComponent<TextFader>().enabled = false;
            tutorialCanvas.transform.GetChild(2).GetChild(1).GetComponent<AudioSource>().Stop();

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

        //音再生
        text.GetComponent<AudioSource>().Play();
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
