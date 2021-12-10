using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectCanvasInterface : MonoBehaviour
{
    [SerializeField]
    GameObject charaSimpleDataUI;//キャラデータ簡易表示UI

    [SerializeField]
    GameObject charaPrefab;

    [SerializeField]
    GameObject charaListParent;


    [SerializeField]
    protected GameObject ListUI;

    [SerializeField]
    protected GameObject startButton;

    List<GameObject> CharaList;


    [SerializeField]
    protected GameObject[] selectChara = new GameObject[2];
    
    int selectFrag;//0→一人目、1→二人目


    [SerializeField]
    GameObject select = null;

    [SerializeField]
    protected SelectScrollbar listScrollbar;

    [SerializeField]
    protected GameObject startAnimationCanvas;

    [SerializeField]
    protected GameObject mainCanvas;

    protected CameraController cameraController;

    virtual public void Initialize()
    {
        cameraController = Camera.main.GetComponent<CameraController>();

        CharaList = new List<GameObject>();
        //CharaList = CharacterManager.Instance.characterList;

        CreateCharaList();

        startButton.SetActive(false);

        if(startAnimationCanvas != null)
            startAnimationCanvas.SetActive(false);
        ListUI.SetActive(false);
        
    }

    public void DisplayCharaList()
    {
        if (!MouseManager.Instance.OnDoubleClickUI()) return;

        ListUI.SetActive(true);

        listScrollbar.ScrollbarPositionReset();
    }



    public void Selected_1(){ selectFrag = 0; }

    public void Selected_2(){ selectFrag = 1; }


    virtual public void CharacterDicision()
    {
        if (select == null) return;
        int frag = (selectFrag == 0) ? 1 : 0;
        //if (selectChara[frag].GetComponent<SelectCharacterDataInterface>().GetSelectGameObject() != null)
        //{
        //    if(select != null)
        //    {
        //        if(selectChara[frag].GetComponent<SelectCharacterDataInterface>().GetSelectGameObject()
        //            ==
        //            select.GetComponent<SelectCharacterDataInterface>().GetSelectGameObject())
        //        {
        //            Debug.Log("キャラ重複");
        //            return;
        //        }
        //    }
        //}
        if (selectChara[frag].GetComponent<SelectCharacterDataInterface>().GetSelectGameObject() != null 
            &&
            select == selectChara[frag].GetComponent<SelectCharacterDataInterface>().GetSelectGameObject())
        {
            //重複
            return;
        }


        //else if(selectChara[selectFrag].GetComponent<SelectCharacterDataInterface>().GetSelectGameObject() != null)
        //{

        //}

        SetCharactarData();

        ListUI.SetActive(false);

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
        setChara.GetComponent<SelectCharacterDataInterface>().SetData(ref select);


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

        //string name = data.data.GetName;
        //int r = data.data.GetResearch;
        //int p = data.data.GetProduction;
        //int m = data.data.GetManagement;
        //int inv = data.data.GetInvestigation;
        //Sprite sprite = data.data.GetSprite;

        charaSimpleDataUI.GetComponent<ActionCharacterInterface>().SetData(data.data);
    }

    virtual public void StartButton(){}

    public void SelectCancel(){ ListUI.SetActive(false);}

    public void CreateCharaList()
    {
        //CharaList = CharacterManager.Instance.characterList;

        //リスト生成
        for (int i = 0; i < CharacterManager.Instance.characterList.Count; i++)
        {
            GameObject obj = Instantiate(charaPrefab);
            //obj.name = obj.name.Replace("(Clone)", "");
            //obj.name += " " + i;

            //CharacterData data = CharacterManager.Instance.characterList[i].GetComponent<CharacterData>();

            obj.transform.SetParent(charaListParent.transform,false);
            obj.gameObject.name = "CharaData[" + CharacterManager.Instance.characterList[i].GetComponent<CharacterData>().name + "]";


            //int r = CharacterManager.Instance.characterList[i].GetComponent<CharacterData>().research;
            //int p = CharacterManager.Instance.characterList[i].GetComponent<CharacterData>().production;
            //int m = CharacterManager.Instance.characterList[i].GetComponent<CharacterData>().management;
            //int inv = CharacterManager.Instance.characterList[i].GetComponent<CharacterData>().investigation;
            //string name = CharacterManager.Instance.characterList[i].GetComponent<CharacterData>().name;
            //Sprite sprite = CharacterManager.Instance.characterList[i].GetComponent<CharacterData>().characterSprite;

            obj.GetComponent<ActionCharacterInterface>().Create(CharacterManager.Instance.characterList[i]);
            //obj.GetComponent<ActionCharacterInterface>().Create();

            CharaList.Add(obj);
        }
    }
}
