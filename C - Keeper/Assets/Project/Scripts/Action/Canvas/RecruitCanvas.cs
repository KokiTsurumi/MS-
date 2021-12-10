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

    [SerializeField] GameObject mainCanvas;

    /// <summary>
    /// originalデータのポインタ
    /// </summary>
    GameObject[] dispCharacter;


    void Start()
    {
        startButton.SetActive(false);
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

        if (dispCharacter[selectFrag] != null)
        {
            dispCharacter[selectFrag].GetComponent<RecruitCharacterData>().selected = false;
        }

        dispCharacter[selectFrag] = recruitCharacterListUI.GetComponent<RecruitSelectCanvas>().GetDisplayCharacter;
        selectChara[selectFrag].GetComponent<RecruitCharacterData>().SetCharacterData(dispCharacter[selectFrag].GetComponent<RecruitCharacterData>());
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

    public void SimpleCharaDataDisplay()
    {
        RecruitCharacterData data = selectChara[selectFrag].GetComponent<RecruitCharacterData>();
        if (data.GetNullCheck == false) return;

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
}
