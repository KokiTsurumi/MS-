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

    [SerializeField]
    List<GameObject> CharaList;

    [SerializeField]
    protected List<GameObject> RobotList;

    [SerializeField]
    GameObject[] selectChara = new GameObject[2];
    
    int selectFrag;//0→一人目、1→二人目


    [SerializeField]

    GameObject select = null;

    [SerializeField]
    protected SelectScrollbar listScrollbar;

    virtual public void Initialize()
    {

        CharaList = CharacterManager.Instance.CharacterList;

        CreateCharaList();

        startButton.SetActive(false);
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

        string name = data.GetName();
        int r = data.GetResearch();
        int p = data.GetProduction();
        int m = data.GetManagement();
        int inv = data.GetInvestigation();

        charaSimpleDataUI.GetComponent<ActionCharacterInterface>().SetData(name, r, p, m, inv);
    }

    virtual public void StartButton()
    {
        Camera.main.GetComponent<CameraController>().GetCurrntAction().ActionEnd();
    }

    public void SelectCancel(){ ListUI.SetActive(false);}

    public void CreateCharaList()
    {
        CharaList = CharacterManager.Instance.CharacterList;

        //リスト生成
        for (int i = 0; i < CharaList.Count; i++)
        {
            GameObject obj = (GameObject)Instantiate(charaPrefab);
            //obj.name = obj.name.Replace("(Clone)", "");
            //obj.name += " " + i;

            CharacterData data = CharaList[i].GetComponent<CharacterData>();

            obj.transform.SetParent(charaListParent.transform);
            obj.name = "CharaData[" + data.name + "]";


            int r = data.research;
            int p = data.production;
            int m = data.management;
            int inv = data.investigation;
            string name = data.name;

            obj.GetComponent<ActionCharacterInterface>().SetData(name, r, p, m, inv);
            obj.GetComponent<ActionCharacterInterface>().Create();
        }
    }
}
