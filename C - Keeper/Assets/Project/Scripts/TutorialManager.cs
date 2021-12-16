using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// �@�`���[�g���A���@�V���O���g���@�N���X
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
        InvestigationStart,
        Investigation,
        /// <summary>
        /// �Z�l�̐��𒮂�
        /// </summary>
        //InformationStart,
        Information,
        /// <summary>
        /// ���Y��
        /// </summary>
        Production,
        ProductionRobotCreate,
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

    public delegate void DelegateFunc();
    public DelegateFunc delegateFunc;

    delegate void Coroutine();

    void Start()
    {
        //�Ȃ����Ⴄ������������o�O���C������

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
            {
                tutorialCanvas.transform.GetChild(1).GetChild(2).GetComponent<Image>().enabled = false;

            }
            else
            {
                tutorialCanvas.transform.GetChild(1).GetChild(2).GetComponent<Image>().enabled = true;
                tutorialCanvas.transform.GetChild(1).GetChild(1).GetComponent<AudioSource>().Stop();
            }

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
        stepList.AddListText("�͂��߂܂��āI���ꂩ�炱�̑g�D�̔鏑��w�߂܂�(�鏑�q)�ł��I");
        stepList.AddListText("���̑g�D�́A����Ă��܂����C����Y��ɂ��A���Ȃ��Ȃ������������Ăі߂��g�D�ł��I");
        stepList.AddListText("����ł͍ŏ��ɂ��̑g�D�œ����Ă��炤�l�����ق��܂��傤�I");
        stepList.AddListFunc(RecruitStart);
        stepList.AddListFunc(RecruitEnd);
        stepList.AddListText("���X�����l�ނ�I�т܂����ˁI");
        stepList.AddListText("����ł͑�������2�l�Ɏd��������U��܂��傤�I");
        stepList.AddListText("�܂��ŏ��ɓ��̏󋵂�c�����܂��傤�I");
        stepList.AddListText("�����N���b�N���Ă��������I");
        stepList.AddListFunc(InvestigationZoomIn);
        stepList.AddListFunc(InvestigationTextStart);
        stepList.AddListText("���ɃR�}���h������I�����Ă�������");
        stepList.AddListFunc(InvestigationTextEnd);
        stepList.AddListFunc(InvestigationStart);
        stepList.AddListText("�󂫂��N���b�N����Ɛl�ނ��z�u�ł��܂���I");
        stepList.AddListFunc(InvestigationSelectText);
        stepList.AddListText("�l�ޔz�u�����ł��I");
        stepList.AddListText("�����␴�|���s���Ǝ��Ԃ�������̂ŁA�����܂ŏ��X���҂����������ˁI");
        stepList.AddListFunc(InvestigationTimerStart);
        stepList.AddListFunc(InvestigationEnd);
        stepList.AddListText("�������܂����I                  ");
        stepList.AddListText("�������s���ƁA���̓��͂ǂꂾ����������Ă���̂��ƏZ���̐��𕷂������o���܂��I");
        stepList.AddListFunc(InformationStart);
        stepList.AddListFunc(Information);
        stepList.AddListText("���̓��ł̓S�~���ɔY�܂���Ă���悤�ł��ˁE�E�E");
        stepList.AddListFunc(InformationText);
        stepList.AddListFunc(InformationEnd);
        stepList.AddListText("����ł̓��{�b�g�𐶎Y���Ă�����g���Đ��|���܂��傤�I");
        stepList.AddListText("����̃R�}���h���琶�Y���N���b�N���Ă��������I");
        stepList.AddListFunc(ProductionPopOut);
        stepList.AddListFunc(ProductionStart);
        stepList.AddListText("�󂢂Ă���ꏊ���N���b�N���Đl�ނ�z�u���Ă��������I");
        stepList.AddListFunc(ProductionSelectText);
        stepList.AddListText("���Y�̔z�u�����ł��I���{�b�g�����Y�����܂ŏ��X���҂����������I");
        stepList.AddListFunc(ProductionTimerStart);
        stepList.AddListFunc(ProductionEnd);
        stepList.AddListText("���{�b�g���������܂����I�����N���b�N���ă��{�b�g�����Ă݂܂��傤");
        stepList.AddListFunc(ProductionTextEnd);
        stepList.AddListFunc(CleaningTextStart);
        stepList.AddListText("����ł̓��{�b�g��z�u���Đ��|���J�n���܂��傤�I�A�܂��͐��|�̃R�}���h��I�����Ă��������I");
        stepList.AddListFunc(CleaningPopOut);
        stepList.AddListFunc(CleaningStart);
        stepList.AddListText("���|�J�n�ł��A���΂炭���҂����������I");
        stepList.AddListFunc(CleaningTimerStart);
        stepList.AddListFunc(CleaningEnd);
        stepList.AddListFunc(SeaDissolveStart);
        stepList.AddListFunc(SeaDissolveEnd);
        stepList.AddListText("���|���I���܂����I�A�����x�́A�����N���b�N������ɓ����N���b�N����΂킩��悤�ɂȂ��Ă��܂��I");
        stepList.AddListText("�����Ă��̓��̉����x��0%�ɂȂ�A���ꂢ�ȊC�ɖ߂�܂����I");
        stepList.AddListFunc(RankUp);
        stepList.AddListText("�m���x�����N���オ��܂����I");
        stepList.AddListText("�m���x���オ�������ƂŁA�g�D�ɓ��肽���ƌ����Ă���������Ă��܂��I�N���ق��܂����H");
        stepList.AddListFunc(RankUpRecruitStart);
        stepList.AddListFunc(RankUpRecruitEnd);
        stepList.AddListText("�����Ǝv���܂��I����ŏ����͊������܂����I");
        stepList.AddListText("���̊C��̉�����S�Ď�菜���΁A���Ԍn�͌��ɖ߂�܂��I����ł́A�撣���ĉ������I");
        stepList.AddListFunc(TutorialEnd);

        //EventTrigger�Z�b�g
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventDate) => TextNextStep());
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
        //tutorialState = TutorialState.Investigation;
        tutorialState = TutorialState.InvestigationStart;
        //Camera.main.GetComponent<CameraController>().ZoomIn();
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
        //NextStep();
    }

    void InvestigationStart()
    {
        tutorialCanvas.SetActive(true);
        tutorialState = TutorialState.Investigation;
        investigationCanvas = Instantiate(investigationCanvasPrefab);
        investigationCanvas.GetComponent<InvestigationCanvas>().Initialize();
        updateFunc = InvestigationUpdate;
        NextStep();
    }

    void InvestigationSelectText()
    {
        tutorialCanvas.SetActive(false);

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
        //tutorialState = TutorialState.InformationStart;
        //Camera.main.GetComponent<CameraController>().ZoomIn();
    }

    void Information()
    {
        //IslandManager.Instance.GetCurrentIsland().GetComponent<IslandBase>().icon.SetActive(true);
        tutorialState = TutorialState.Information;
        informationPop = Instantiate(informationPopPrefab);
        informationPop.GetComponent<InformationPop>().Create(IslandManager.Instance.GetCurrentIsland());
        IslandManager.Instance.GetCurrentIsland().GetComponent<IslandBase>().icon.GetComponent<Canvas>().enabled = false;
        NextStep();
        tutorialCanvas.SetActive(true);
    }

    //void InformationText()
    //{
    //    tutorialCanvas.SetActive(false);
    //    NextStep();
    //}

    void InformationText()
    {
        //IslandManager.Instance.GetCurrentIsland().GetComponent<IslandBase>().icon.SetActive(true);
        tutorialCanvas.SetActive(false);
        //NextStep();
    }

    void InformationEnd()
    {
        Camera.main.GetComponent<CameraController>().ZoomOut();
        StartCoroutine(CoroutineTimer(0.8f, InformationEndTimer));
    }

    void InformationEndTimer()
    {
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
        tutorialCanvas.SetActive(true);
        NextStep();
    }

    void ProductionSelectText()
    {
        tutorialCanvas.SetActive(false);


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
        //Destroy(productionCanvas);
        tutorialCanvas.SetActive(false);
        //NextStep();
    }

    void CleaningTextStart()
    {
        //Camera.main.GetComponent<CameraController>().ZoomOut();
        NextStep();
        tutorialCanvas.SetActive(true);
        tutorialState = TutorialState.Cleanning;
    }


    void CleaningPopOut()
    {
        tutorialCanvas.SetActive(false);
        Camera.main.GetComponent<CameraController>().ZoomIn();
    }

    void CleaningStart()
    {
        cleaningCanvas = Instantiate(cleaningCanvasPrefab);
        cleaningCanvas.GetComponent<CleaningCanvas>().Initialize();
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
        //tutorialCanvas.SetActive(true);
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
        //�m���x�����N�PUp�̂��߂̒���
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
        //Destroy(this.gameObject);//����ł�����
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

        //�t�F�[�f�B���O��
        if (tutorialCanvas.transform.GetChild(1).GetChild(1).GetComponent<TextFader>().enabled == true)
        {
            tutorialCanvas.transform.GetChild(1).GetChild(1).GetComponent<TextFader>().enabled = false;
            tutorialCanvas.transform.GetChild(1).GetChild(1).GetComponent<AudioSource>().Stop();

        }
        //�t�F�[�f�B���O��
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
        //�\��
        text.GetComponent<Text>().text = str;
        text.GetComponent<TextFader>().enabled = true;

        //���Đ�
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
