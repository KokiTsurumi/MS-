using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChoiceHumanCanvasSC : MonoBehaviour
{
    [SerializeField]
    GameObject characterData;//�L�����f�[�^���ȈՕ\��������UI

    [SerializeField]
    GameObject selectCharacterPrefab;

    [SerializeField]
    GameObject characterListParent;

    [SerializeField]
    GameObject Human_1 = null;
    [SerializeField]
    GameObject Human_2 = null;

    [SerializeField]
    GameObject List;//UI

    [SerializeField]
    GameObject startButton;

    List<GameObject> CharacterList;



    public GameObject select = null;

    bool setCharacterFrag;//��l�ڂ���l�ڂ𔻒f����t���O
    bool isSelectFrag1, isSelectFrag2;//�Z�b�g���ꂽ�����f����t���O



    void Start()
    {
        List.SetActive(false);

        CharacterList = CharacterManager.Instance.CharacterList;

        isSelectFrag1 = false;
        isSelectFrag2 = false;

        startButton.SetActive(false);
    }

 

    public void Choice1OnClick()
    {
        //�_�u���N���b�N�ŃL�����I��
        if(MouseManagerSC.Instance.OnDoubleClick())
        {
            ListDisplay();
            setCharacterFrag = true;
        }
        //�V���O���N���b�N�ŊȈՕ\��
        else
        {
            if(isSelectFrag1)
                CharacterDataDisplay(ref Human_1);
        }
  
    }

    public void Choice2OnClick()
    {
        //�_�u���N���b�N�ŃL�����I��
        if(MouseManagerSC.Instance.OnDoubleClick())
        {
            ListDisplay();
            setCharacterFrag = false;
        }
        //�V���O���N���b�N�ŊȈՕ\��
        else
        {
            if(isSelectFrag2)
                CharacterDataDisplay(ref Human_2);
        }

        
    }

    
    void ListDisplay()
    {
        List.SetActive(true);


        for (int i = 0; i < CharacterList.Count; i++)
        {
            GameObject obj = Instantiate(selectCharacterPrefab);
            
            obj.transform.parent = characterListParent.transform;

            int r = CharacterList[i].GetComponent<CharacterData>().research;
            int p = CharacterList[i].GetComponent<CharacterData>().production;
            int m = CharacterList[i].GetComponent<CharacterData>().management;
            int inv = CharacterList[i].GetComponent<CharacterData>().investigation;
            string name = CharacterList[i].GetComponent<CharacterData>().name;

            obj.GetComponent<CharacterSelectDataSC>().SetData(name,r, p, m, inv);
            obj.GetComponent<CharacterSelectDataSC>().canvas = this;
        }


        return;
    }

    //�I�𒆂̃L�����N�^�[�I�u�W�F�N�g���擾
    public void SelectCharacterObject()
    {
        select = EventSystem.current.currentSelectedGameObject;
        return;
    }

    public void CharacterDicision()
    {

        if (setCharacterFrag)
        {
            //��l��
            SetCharactarData(ref Human_1);
            CharacterDataDisplay(ref Human_1);
            isSelectFrag1 = true;
        }
        else
        {
            //��l��
            SetCharactarData(ref Human_2);
            CharacterDataDisplay(ref Human_2);
            isSelectFrag2 = true;
        }

        //�I���ς݂̃L�����N�^�[�͑I�𒆂ƕ�����悤�ɐF��ω��������肷��
        //�I���ς݂͑I�������Ȃ��悤�ɂ���

        List.SetActive(false);

        if (isSelectFrag1 && isSelectFrag2)
        {
            startButton.SetActive(true);
        }

    }

    void SetCharactarData(ref GameObject human)
    {

        CharacterSelectDataSC characterData = select.GetComponentInParent<CharacterSelectDataSC>();
        GameObject characterImage = select.transform.GetChild(0).gameObject;

        //�o�b�N�O���E���h
        //human.GetComponent<Image>().sprite = characterImage.transform.GetChild(0).GetComponent<Image>().sprite;
        human.GetComponent<Image>().color = characterImage.transform.GetChild(0).GetComponent<Image>().color;

        //�L�����N�^�[
        human.transform.GetChild(0).GetComponent<Image>().sprite = characterImage.transform.GetChild(1).GetComponent<Image>().sprite;

        //�f�[�^�擾
        human.GetComponent<choiceHumanSC>().SetData(characterData.GetName, characterData.GetResearch, characterData.GetProduction, characterData.GetManagement, characterData.GetInvestigation);
    }

 
    void CharacterDataDisplay(ref GameObject human)
    {
        if (human == null) return;

        choiceHumanSC data = human.GetComponent<choiceHumanSC>();

        /*characterData�̂��̏���
         * 
         * backGround   (0)
         * name         (1)
         * research     (2)
         * production   (3)
         * management   (4)
         * investigation(5)
         */

        //  name
        characterData.transform.GetChild(1).GetComponent<Text>().text = data.GetName;
        //research
        characterData.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = CharacterManager.Instance.RankTransfer(data.GetResearch);
        //production
        characterData.transform.GetChild(3).GetChild(1).GetComponent<Text>().text = CharacterManager.Instance.RankTransfer(data.GetProduction);
        //management
        characterData.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = CharacterManager.Instance.RankTransfer(data.GetManagement);
        //investigation
        characterData.transform.GetChild(5).GetChild(1).GetComponent<Text>().text = CharacterManager.Instance.RankTransfer(data.GetInvestigation);


    }

    public void StartButton()
    {
        //�^�b�O�v�Z����
        //�^�C�}�[�v�Z����

        List.SetActive(false);

    }

    public void SelectCancel()
    {
        List.SetActive(false);
    }

  
}
