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

    [SerializeField] GameObject recruitCanvas;

    [SerializeField] GameObject mainCanvas;

    [SerializeField] GameObject navigatorCanvas;

    [SerializeField] GameObject startButton;


    [SerializeField] GameObject[] selectChara = new GameObject[2];

    [System.NonSerialized] public int selectFrag;//0→一人目、1→二人目

    /// <summary>
    /// originalデータのポインタ
    /// </summary>
    GameObject[] dispCharacter = new GameObject[2];


    void Start()
    {
        startButton.SetActive(false);
        recruitCanvas.SetActive(false);
        mainCanvas.SetActive(false);
        navigatorCanvas.SetActive(true);
    }

    public void DisplayCharaList()
    {
        if (!MouseManager.Instance.OnDoubleClickUI()) return;

        recruitCanvas.SetActive(true);
    }


    public void Selected_1() { selectFrag = 0; }
    public void Selected_2() { selectFrag = 1; }

    public void SetCharactarData()
    {

        if (dispCharacter[selectFrag] != null)
        {
            dispCharacter[selectFrag].GetComponent<RecruitCharacterData>().selected = false;
        }

        dispCharacter[selectFrag] = recruitCanvas.GetComponent<RecruitSelectCanvas>().GetDisplayCharacter;
        selectChara[selectFrag].GetComponent<RecruitCharacterData>().SetCharacterData(dispCharacter[selectFrag].GetComponent<RecruitCharacterData>());
        dispCharacter[selectFrag].GetComponent<RecruitCharacterData>().selected = true;

        
        SimpleCharaDataDisplay();

        recruitCanvas.SetActive(false);

        if(dispCharacter[0] != null 
           &&
           dispCharacter[1] != null)
        {
            startButton.SetActive(true);
        }
    }

    public void SimpleCharaDataDisplay()
    {
        RecruitCharacterData data = selectChara[selectFrag].GetComponent<RecruitCharacterData>();
        if (data.GetNullCheck == false) return;

        charaSimpleDataUI.GetComponent<RecruitCharacterData>().SetCharacterData(data);
    }

    public void StartButton()
    {
        CharacterManager.Instance.HireCharacter(dispCharacter[0].GetComponent<RecruitCharacterData>().GetOriginal, dispCharacter[1].GetComponent<RecruitCharacterData>().GetOriginal);

        //if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Recruit)
        //{
        //    TutorialManager.Instance.NextStep();
        //}
        //else if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.RankUpRecruit)
        //{
        //    TutorialManager.Instance.NextStep();
        //}


        RankUpUI.Instance.useCanvas = false;


        //新しく人材を迎えたよ！てきなUI表示
        {
            //RankUpUI.Instance.RankUpCheck();//ランクが上がっていたらさらに人材選択


        }

        mainCanvas.SetActive(false);
        navigatorCanvas.GetComponent<RecruitNavigatorCanvas>().RecruitEnd();
        //Destroy(this.gameObject);
    }


}
