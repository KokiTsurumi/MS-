using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProductionCanvas : MonoBehaviour
{
    [SerializeField]
    GameObject charaSimpleDataUI;//キャラデータ簡易表示UI

    [SerializeField]
    GameObject selectCharaPrefab;

    [SerializeField]
    GameObject charaListParent;

    
    [SerializeField]
    GameObject ListUI;//UI

    [SerializeField]
    GameObject startButton;

    List<GameObject> CharaList;

    [SerializeField]    
    GameObject[] selectChara = new GameObject[2];
    int selectCharacterFrag;



    public GameObject select = null;

    bool setCharacterFrag;//一人目か二人目を判断するフラグ

    [SerializeField]
    SelectScrollbar charaListScrollbar;

    void Start()
    {
        ListUI.SetActive(false);

        CharaList = CharacterManager.Instance.CharacterList;

        startButton.SetActive(false);
    }



    public void DisplayCharaList()
    {
        if (! MouseManagerSC.Instance.OnDoubleClick()) return;

        ListUI.SetActive(true);

        charaListScrollbar.ScrollbarPositionReset();
    }



    public void Selected_1()
    {
        selectCharacterFrag = 0;
    }

    public void Selected_2()
    {
        selectCharacterFrag = 1;
    }

    
    public void CharacterDicision()
    {
        
        SetCharactarData();

        ListUI.SetActive(false);

        if (selectChara[0].GetComponent<SelectCharacterData>().GetSelectGameObject() != null 
            &&
            selectChara[1].GetComponent<SelectCharacterData>().GetSelectGameObject() != null)
        {
            startButton.SetActive(true);
        }

    }

    void SetCharactarData()
    {
        GameObject setChara = selectChara[selectCharacterFrag];

        //データセット
        setChara.GetComponent<SelectCharacterData>().SetData(ref select);


        SimpleCharaDataDisplay();
    }

    public void CharaDataBack()
    {
        select =  MouseManagerSC.Instance.GetCurrentSelectedGameObject();
    }

    public void SimpleCharaDataDisplay()
    {

        if (selectChara[selectCharacterFrag].GetComponent<SelectCharacterData>().GetSelectGameObject() == null) return;

        SelectCharacterData data = selectChara[selectCharacterFrag].GetComponent<SelectCharacterData>();

        string name = data.GetData<ProductionCharacterData>().GetName;
        int r = data.GetData<ProductionCharacterData>().GetResearch;
        int p = data.GetData<ProductionCharacterData>().GetProduction;
        int m = data.GetData<ProductionCharacterData>().GetManagement;
        int inv = data.GetData<ProductionCharacterData>().GetInvestigation;

        charaSimpleDataUI.GetComponent<InvestigationCharacterData>().SetData(name, r, p, m, inv);
    }

    public void StartButton()
    {
        //タッグ計算処理
        //タイマー計算処理
        //島のスクリプト内にある調査済みのboolをture（汚染度表示に利用）

        this.gameObject.SetActive(false);

    }

    public void SelectCancel()
    {
        ListUI.SetActive(false);
    }

    public void CreateCharaList()
    {
        CharaList = CharacterManager.Instance.CharacterList;

        //リスト生成
        for (int i = 0; i < CharaList.Count; i++)
        {
            GameObject obj = (GameObject)Instantiate(selectCharaPrefab);
            obj.name = obj.name.Replace("(Clone)", "");
            obj.name += " " + i;

            CharacterData data = CharaList[i].GetComponent<CharacterData>();

            obj.transform.SetParent(charaListParent.transform);

            int r = data.research;
            int p = data.production;
            int m = data.management;
            int inv = data.investigation;
            string name = data.name;

            obj.GetComponent<InvestigationCharacterData>().SetData(name, r, p, m, inv);
            obj.GetComponent<InvestigationCharacterData>().Create();
        }
    }
}
