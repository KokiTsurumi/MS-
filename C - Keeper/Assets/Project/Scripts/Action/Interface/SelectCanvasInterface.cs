using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// キャラクター選択　UI　インターフェース　クラス
/// </summary>
public class SelectCanvasInterface : MonoBehaviour
{
    [SerializeField] GameObject charaSimpleDataUI;//キャラデータ簡易表示UI

    [SerializeField] GameObject charaPrefab;

    [SerializeField] GameObject charaListParent;

    [SerializeField] protected GameObject ListUI;

    [SerializeField] protected GameObject startButton;

    [SerializeField] protected GameObject[] selectChara = new GameObject[2];

    [SerializeField] GameObject select = null;

    [SerializeField] protected SelectScrollbar listScrollbar;

    [SerializeField] protected GameObject startAnimationCanvas;

    [SerializeField] protected GameObject mainCanvas;



    List<GameObject> CharaList;
    int selectFrag;//0→一人目、1→二人目
    protected CameraController cameraController;

    virtual public void Initialize()
    {
        cameraController = Camera.main.GetComponent<CameraController>();

        CharaList = new List<GameObject>();

        CreateCharaList();

        startButton.SetActive(false);

        if(startAnimationCanvas != null)
            startAnimationCanvas.SetActive(false);
        ListUI.SetActive(false);

        if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.No)
            cameraController.GetComponent<CameraController>().backButton.SetActive(true);
    }

    public void DisplayCharaList()
    {
        if (!MouseManager.Instance.OnDoubleClickUI()) return;

        ListUI.SetActive(true);

        if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.No)
            cameraController.GetComponent<CameraController>().backButton.SetActive(false);

        listScrollbar.ScrollbarPositionReset();
    }


    public void Selected_1(){ selectFrag = 0; }
    public void Selected_2(){ selectFrag = 1; }

    virtual public void CharacterDicision()
    {
        if (select == null) return;
        int frag = (selectFrag == 0) ? 1 : 0;
        if (selectChara[frag].GetComponent<SelectCharacterDataInterface>().GetSelectGameObject() != null 
            &&
            select == selectChara[frag].GetComponent<SelectCharacterDataInterface>().GetSelectGameObject())
        {
            //重複
            return;
        }

        SetCharactarData();

        ListUI.SetActive(false);

        if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.No)
            cameraController.GetComponent<CameraController>().backButton.SetActive(true);

        if (selectChara[0].GetComponent<SelectCharacterDataInterface>().GetSelectGameObject() != null
            &&
            selectChara[1].GetComponent<SelectCharacterDataInterface>().GetSelectGameObject() != null)
        {
            startButton.SetActive(true);
        }
    }

    virtual public void SetCharactarData()
    {
        GameObject setChara = selectChara[selectFrag];

        //データセット
        setChara.GetComponent<SelectCharacterDataInterface>().SetData(select);

        SimpleCharaDataDisplay();
    }

    public void CharaDataBack()
    {
        select = MouseManager.Instance.GetCurrentSelectedGameObject();
    }

    public void SimpleCharaDataDisplay()
    {
        SelectCharacterDataInterface data = selectChara[selectFrag].GetComponent<SelectCharacterDataInterface>();
        if (data.GetSelectGameObject() == null) return;

        charaSimpleDataUI.GetComponent<ActionCharacterInterface>().SetData(data);
    }

    virtual public void StartButton(){}

    public void SelectCancel()
    {
        ListUI.SetActive(false);

        if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.No)
            cameraController.GetComponent<CameraController>().backButton.SetActive(true);

    }

    public void CreateCharaList()
    {
        //リスト生成
        for (int i = 0; i < CharacterManager.Instance.characterList.Count; i++)
        {
            GameObject obj = Instantiate(charaPrefab);

            obj.transform.SetParent(charaListParent.transform,false);
            obj.gameObject.name = "CharaData[" + CharacterManager.Instance.characterList[i].GetComponent<CharacterData>().name + "]";
            obj.GetComponent<ActionCharacterInterface>().Create(CharacterManager.Instance.characterList[i]);

            CharaList.Add(obj);
        }
    }
}
