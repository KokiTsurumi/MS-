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

    //CharacterManager？から全従業員のGameObjectを参照してリスト表示させる
    void ListDisplay()
    {
        List.SetActive(true);


        //一人目で選んだ人は二人目では表示させないようにする


        return;
    }

    //選択中のキャラクターオブジェクトを取得
    public void SelectCharacterObject()
    {
        select = eventSystem.currentSelectedGameObject;
        return;
    }

    public void CharacterDicision()
    {

        if(setCharacterFrag)
        {
            //一人目
            SetCharactarData(ref Human_1);
        }
        else
        {
            //二人目
            SetCharactarData(ref Human_2);
        }

        List.SetActive(false);
    }

    void SetCharactarData(ref GameObject human)
    {

        GameObject characterImage = select.transform.GetChild(0).gameObject;

        //バックグラウンド
        //human.GetComponent<Image>().sprite = characterImage.transform.GetChild(0).GetComponent<Image>().sprite;
        human.GetComponent<Image>().color = characterImage.transform.GetChild(0).GetComponent<Image>().color;

        //キャラクター
        human.transform.GetChild(0).GetComponent<Image>().sprite = characterImage.transform.GetChild(1).GetComponent<Image>().sprite;

    }
}
