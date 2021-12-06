using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : SingletonMonoBehaviour<TutorialManager>
{
    [SerializeField]
    GameObject navigatorText;
    

    //List<string> textList;

    //int listNumber = 0;


    [SerializeField]
    GameObject tutorialCanvas;

    [SerializeField]
    GameObject recruitCanvas;

    [SerializeField]
    GameObject investigationCanvas;

    [SerializeField]
    GameObject productionCanvas;


    public enum TutorialState
    {
        No,
        Investigation,
        Production,
        Cleanning
    }

    public TutorialState tutorialState = TutorialState.No;


    StepList stepList;



    delegate void UpdateFunc();
    UpdateFunc updateFunc;

    delegate void Coroutine();

    void Start()
    {
        //�Ȃ����Ⴄ������������o�O���C������

        recruitCanvas.SetActive(false);


        stepList = new StepList(navigatorText);


        stepList.AddListFunc(TutorialStart);
        stepList.AddListText("�l�ނ�I�����܂�");
        stepList.AddListFunc(RecruitStart);
        stepList.AddListFunc(RecruitEnd);
        stepList.AddListText("���͒������s���܂�");
        stepList.AddListFunc(InvestigationZoomIn);
        stepList.AddListFunc(InvestigationStart);
        stepList.AddListFunc(InvestigationEnd);
        stepList.AddListText("���������ł�\n���͐��Y���s���܂��傤w");
        stepList.AddListFunc(ProductionPopOut);
        stepList.AddListFunc(ProductionStart);


        updateFunc = TutorialUpdateNull;


        //���͂�\��
        //�֐������s
        //�������̕ϐ��ɓ��ꂽ��
        //�������C���N�������g���ăX�e�b�v�i�s




        StartCoroutine(CoroutineTimer(0.5f,TutorialStart));

    }

    void Update()
    {
        
        updateFunc();

    }


    

    void TutorialUpdateNull()
    {
      
    }

    void TutorialStart()
    {
        tutorialCanvas.SetActive(true);
        stepList.Next();
        stepList.Step();

        //�ŏ��ɑI������l�ނ𐶐�
        CharacterManager.Instance.CreateCandidateCharacter();
    }

    void RecruitStart()
    {
        tutorialCanvas.SetActive(false);


        recruitCanvas.GetComponent<RecruitCanvas>().DisplayRecruitCharacterList();
        recruitCanvas.SetActive(true);
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
        Instantiate(investigationCanvas).GetComponent<InvestigationCanvas>().Initialize();

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

        //updateFunc = InvestigationEndUpdate;
        stepList.Next();
        stepList.Step();
        tutorialCanvas.SetActive(true);

    }

    void ProductionPopOut()
    {
        tutorialCanvas.SetActive(false);
        tutorialState = TutorialState.Production;
        Camera.main.GetComponent<CameraController>().ZoomIn();
    }

    void ProductionStart()
    {

        Instantiate(productionCanvas).GetComponent<ProductionCanvas>().Initialize();
        //tutorialState = TutorialState.Production;
    }

    //�ŏ��Ɍق��L�����N�^�[��\��������
    public void RecruitCharacterDisplay()
    {
        //�L�����o�XSetActive true
    }

    //public void OnClickNextText()
    //{

    //    stepList.NextStep();
    //}

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
