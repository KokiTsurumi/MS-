using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvestigationCanvas : MonoBehaviour
{
    [SerializeField]
    GameObject charaSimpleDataUI;//�L�����f�[�^���ȈՕ\��������UI

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

    bool setCharacterFrag;//��l�ڂ���l�ڂ𔻒f����t���O
    bool isSelectFrag1, isSelectFrag2;//�Z�b�g���ꂽ�����f����t���O

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
        //    //��l��
        //    //SetCharactarData(ref Human_1);
        //    //CharacterDataDisplay(ref Human_1);
        //    //isSelectFrag1 = true;
        //}
        //else
        //{
        //    //��l��
        //    //SetCharactarData(ref Human_2);
        //    //CharacterDataDisplay(ref Human_2);
        //    //isSelectFrag2 = true;
        //}

        //�I���ς݂̃L�����N�^�[�͑I�𒆂ƕ�����悤�ɐF��ω��������肷��
        //�I���ς݂͑I�������Ȃ��悤�ɂ���

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
        //�o�b�N�O���E���h
        setChara.GetComponent<Image>().color = characterImage.transform.GetChild(0).GetComponent<Image>().color;

        //�L�����N�^�[
        setChara.transform.GetChild(0).GetComponent<Image>().sprite = characterImage.transform.GetChild(1).GetComponent<Image>().sprite;

        //�f�[�^�擾
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
        //�^�b�O�v�Z����
        //�^�C�}�[�v�Z����

        this.gameObject.SetActive(false);

    }

    public void SelectCancel()
    {
        ListUI.SetActive(false);
    }

    public void CreateCharaList()
    {
        CharaList = CharacterManager.Instance.CharacterList;

        //���X�g����
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
