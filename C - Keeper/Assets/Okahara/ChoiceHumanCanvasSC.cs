using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChoiceHumanCanvasSC : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject Human_1 = null;
    [SerializeField]
    GameObject Human_2 = null;

    [SerializeField]
    GameObject select = null;

    bool setCharacterFrag;

    [SerializeField]
    GameObject List;

    EventSystem eventSystem;

    void Start()
    {
        List.SetActive(false);
        eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 
   

    public void Choice1OnClick()
    {
        ListDisplay();
        setCharacterFrag = true;
    }

    public void Choice2OnClick()
    {
        ListDisplay();
        setCharacterFrag = false;
    }

    //CharacterManager�H����S�]�ƈ���GameObject���Q�Ƃ��ă��X�g�\��������
    void ListDisplay()
    {
        List.SetActive(true);


        //��l�ڂőI�񂾐l�͓�l�ڂł͕\�������Ȃ��悤�ɂ���


        return;
    }

    //�I�𒆂̃L�����N�^�[�I�u�W�F�N�g���擾
    public void SelectCharacterObject()
    {
        select = eventSystem.currentSelectedGameObject;
        return;
    }

    public void CharacterDicision()
    {

        if(setCharacterFrag)
        {
            //��l��
            SetCharactarData(ref Human_1);
        }
        else
        {
            //��l��
            SetCharactarData(ref Human_2);
        }

        List.SetActive(false);
    }

    void SetCharactarData(ref GameObject human)
    {

        GameObject characterImage = select.transform.GetChild(0).gameObject;

        //�o�b�N�O���E���h
        //human.GetComponent<Image>().sprite = characterImage.transform.GetChild(0).GetComponent<Image>().sprite;
        human.GetComponent<Image>().color = characterImage.transform.GetChild(0).GetComponent<Image>().color;

        //�L�����N�^�[
        human.transform.GetChild(0).GetComponent<Image>().sprite = characterImage.transform.GetChild(1).GetComponent<Image>().sprite;

    }
}
