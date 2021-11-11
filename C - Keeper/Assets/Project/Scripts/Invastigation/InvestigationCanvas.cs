using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvestigationCanvas : MonoBehaviour
{
    [SerializeField]
    GameObject charaSimpleDataUI;//キャラデータを簡易表示させるUI

    [SerializeField]
    GameObject selectCharaPrefab;

    [SerializeField]
    GameObject charaListParent;

    
    [SerializeField]
    GameObject ListUI;//UI

    [SerializeField]
    GameObject startButton;

    List<GameObject> CharaList;

    //--------------------
    [SerializeField]    
    GameObject[] selectChara = new GameObject[2];
    int selectCharacterFrag;
    //--------------------
    public GameObject select = null;

    bool setCharacterFrag;//一人目か二人目を判断するフラグ
    bool isSelectFrag1, isSelectFrag2;//セットされたか判断するフラグ

    [SerializeField]
    InvestigationScrollbar charaListScrollbar;

    void Start()
    {
        ListUI.SetActive(false);

        CharaList = CharacterManager.Instance.CharacterList;

        isSelectFrag1 = false;
        isSelectFrag2 = false;

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

        //if (setCharacterFrag)
        //{
        //    //一人目
        //    //SetCharactarData(ref Human_1);
        //    //CharacterDataDisplay(ref Human_1);
        //    //isSelectFrag1 = true;
        //}
        //else
        //{
        //    //二人目
        //    //SetCharactarData(ref Human_2);
        //    //CharacterDataDisplay(ref Human_2);
        //    //isSelectFrag2 = true;
        //}

        //選択済みのキャラクターは選択中と分かるように色を変化させたりする
        //選択済みは選択させないようにする

        SetCharactarData();

        ListUI.SetActive(false);

        if (selectChara[0].GetComponent<SelectCharacterData>().isSelected 
            &&
            selectChara[1].GetComponent<SelectCharacterData>().isSelected)
        {
            startButton.SetActive(true);
        }

    }

    void SetCharactarData()
    {



        GameObject characterImage = select.transform.GetChild(0).gameObject;

        GameObject setChara = selectChara[selectCharacterFrag];
        //バックグラウンド
        setChara.GetComponent<Image>().color = characterImage.transform.GetChild(0).GetComponent<Image>().color;

        //キャラクター
        setChara.transform.GetChild(0).GetComponent<Image>().sprite = characterImage.transform.GetChild(1).GetComponent<Image>().sprite;

        //データ取得
        setChara.GetComponent<SelectCharacterData>().SetData(select.GetComponent<InvestigationCharacterData>(),ref select);



        selectChara[selectCharacterFrag].GetComponent<SelectCharacterData>().isSelected = true;



        SimpleCharaDataDisplay();
    }

    public void CharaDataBack()
    {
        select =  MouseManagerSC.Instance.GetCurrentSelectedGameObject();
    }

    public void SimpleCharaDataDisplay()
    {

        if (! selectChara[selectCharacterFrag].GetComponent<SelectCharacterData>().isSelected) return;

        SelectCharacterData data = selectChara[selectCharacterFrag].GetComponent<SelectCharacterData>();

        string name = data.GetData().GetName;
        int r = data.GetData().GetResearch;
        int p = data.GetData().GetProduction;
        int m = data.GetData().GetManagement;
        int inv = data.GetData().GetInvestigation;


        charaSimpleDataUI.GetComponent<InvestigationCharacterData>().SetData(name, r, p, m, inv);

 
    }

    public void StartButton()
    {
        //タッグ計算処理
        //タイマー計算処理

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
