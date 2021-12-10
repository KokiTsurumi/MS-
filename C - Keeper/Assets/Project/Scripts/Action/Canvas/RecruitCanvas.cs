using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 人材選択　UI　クラス
/// </summary>
public class RecruitCanvas : MonoBehaviour
{
    [SerializeField] GameObject charaSimpleDataUI;//キャラデータ簡易表示UI

    [SerializeField] GameObject recruitCharacterListUI;

    [SerializeField] GameObject startButton;


    [SerializeField] GameObject[] selectChara = new GameObject[2];

    [System.NonSerialized] public int selectFrag;//0→一人目、1→二人目


    /// <summary>
    /// originalデータのポインタ
    /// </summary>
    [SerializeField] GameObject[] dispCharacter;


    [SerializeField] GameObject mainCanvas;


    void Start()
    {

        startButton.SetActive(false);


        Debug.Log("rectuitCanvas Start");


    }

    public void DisplayCharaList()
    {
        if (!MouseManager.Instance.OnDoubleClickUI()) return;

        recruitCharacterListUI.SetActive(true);

    }



    public void Selected_1() { selectFrag = 0; }

    public void Selected_2() { selectFrag = 1; }



    public void SetCharactarData()
    {
        //recruitCharacterListUI.GetComponent<RecruitSelectCanvas>().GetDisplayCharacter.GetComponent<RecruitCharacterData>().selected = false;


        if (dispCharacter[selectFrag] != null)
        {
            dispCharacter[selectFrag].GetComponent<RecruitCharacterData>().selected = false;
        }


        dispCharacter[selectFrag] = recruitCharacterListUI.GetComponent<RecruitSelectCanvas>().GetDisplayCharacter;


        selectChara[selectFrag].GetComponent<RecruitCharacterData>().SetCharacterData(dispCharacter[selectFrag].GetComponent<RecruitCharacterData>());

        //selectChara[selectFrag].GetComponent<RecruitCharacterData>().selected = true;

        dispCharacter[selectFrag].GetComponent<RecruitCharacterData>().selected = true;

        
        SimpleCharaDataDisplay();

        recruitCharacterListUI.SetActive(false);

        if(dispCharacter[0] != null 
            &&
           dispCharacter[1] != null)
        {
            startButton.SetActive(true);
        }
    }

    //public void CharaDataBack()
    //{
    //    select = MouseManager.Instance.GetCurrentSelectedGameObject();
    //}

    public void SimpleCharaDataDisplay()
    {
        RecruitCharacterData data = selectChara[selectFrag].GetComponent<RecruitCharacterData>();
        if (data.GetNullCheck == false) return;

        //string name = data.data.GetName;
        //int r = data.data.GetResearch;
        //int p = data.data.GetProduction;
        //int m = data.data.GetManagement;
        //int inv = data.data.GetInvestigation;
        //Sprite sprite = data.data.GetSprite;
        //RecruitCharacterData data = recruitCharacterListUI.GetComponent<RecruitSelectCanvas>().displayCharacter.GetComponent<RecruitCharacterData>();

        charaSimpleDataUI.GetComponent<RecruitCharacterData>().SetCharacterData(data);
    }

    public void StartButton()
    {

        CharacterManager.Instance.HireCharacter(dispCharacter[0].GetComponent<RecruitCharacterData>().GetOriginal, dispCharacter[1].GetComponent<RecruitCharacterData>().GetOriginal);


        if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Recruit)
        {
            Destroy(this.gameObject);
            TutorialManager.Instance.NextStep();
        }
    }

    //public void SelectCancel() { ListUI.SetActive(false); }

    public void CreateCharaList()
    {
        //CharaList = CharacterManager.Instance.candidateList;

        //リスト生成
        //for (int i = 0; i < CharaList.Count; i++)
        //{
        //    GameObject obj = (GameObject)Instantiate(charaPrefab);
        //    //obj.name = obj.name.Replace("(Clone)", "");
        //    //obj.name += " " + i;

        //    CharacterData data = CharaList[i].GetComponent<CharacterData>();

        //    obj.transform.SetParent(charaListParent.transform, false);
        //    obj.name = "CharaData[" + data.name + "]";


        //    int r = data.research;
        //    int p = data.production;
        //    int m = data.management;
        //    int inv = data.investigation;
        //    string name = data.name;
        //    Sprite sprite = data.characterSprite;

        //    obj.GetComponent<ActionCharacterInterface>().SetData(name, r, p, m, inv, CharaList[i], sprite);
        //    obj.GetComponent<ActionCharacterInterface>().Create();
        //}
    }

    //-------------------------------------------------------------------------------------

    //bool endToggle = false;

    //private void Update()
    //{
    //    if (endToggle) return;

    //    //アニメ終了時
    //    if (startAnimationCanvas.GetComponent<AnimationCallBack>().GetCallBack == true)
    //    {
    //        endToggle = true;
    //        startAnimationCanvas.SetActive(false);

    //        //島のスクリプト内にある調査済みのboolをture（汚染度表示に利用）
    //        GameObject target = IslandManager.Instance.GetCurrentIsland();

    //        //キャラクターセット
    //        CharacterManager.Instance.characterList[0] = selectChara[0].GetComponent<SelectCharacterDataInterface>().data.originalGameObject;
    //        CharacterManager.Instance.characterList[1] = selectChara[1].GetComponent<SelectCharacterDataInterface>().data.originalGameObject;            //タッグ計算処理
    //        float time = CharacterManager.Instance.CalcInvestigationTime();

    //        CharacterManager.Instance.UseCharacter();

    //        if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Investigation)
    //        {
    //            //タイマー計算処理
    //            target.GetComponent<IslandBase>().StartInvestigate(2.0f);//固定
    //            Destroy(this.gameObject);

    //            //TutorialManager.Instance.NextStep();
    //        }
    //        else
    //        {
    //            //タイマー計算処理
    //            target.GetComponent<IslandBase>().StartInvestigate(time);

    //            //カメラ移動
    //            StartCoroutine(ActionEnd());
    //        }

    //    }
    //}

    //public override void StartButton()
    //{


    //    //キャンバス非表示
    //    //cameraController.GetActionCanvas().SetActive(false);
    //    mainCanvas.SetActive(false);
    //    cameraController.backButton.SetActive(false);

    //    //調査開始UI表示
    //    startAnimationCanvas.SetActive(true);




    //}

    //IEnumerator ActionEnd()
    //{
    //    //数秒後にカメラを戻す
    //    yield return new WaitForSeconds(1.0f);

    //    //base.StartButton();
    //    cameraController.ActionEnd();
    //}
}
