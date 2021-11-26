using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitCanvas : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject recruitCharacterPrefab;

    [SerializeField]
    GameObject charactersParent;

    [SerializeField]
    GameObject displayCharacter;

    [SerializeField]
    List<GameObject> recruitList;
    int dispListNumber = 0;

    [SerializeField]
    GameObject selectCanvas;

    GameObject[] selectChara = new GameObject[2];

    //[SerializeField]
    //Button selectButton;
    //[SerializeField]
    //Button backButton;

    [SerializeField]
    Toggle toggle;

    void Start()
    {
        //test
        selectCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //if(selectChara[0] != null && selectChara[1] != null)
        //{
        //    selectCanvas.SetActive(true);
        //}

        //if (recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected)
        //{
        //    selectButton.enabled = false;
        //    backButton.enabled = true;
        //}
        //else
        //{
        //    selectButton.enabled = true;
        //    backButton.enabled = false;
        //}

        //if(displayCharacter.GetComponent<RecruitCharacterData>().selected)
        //{
        //    toggle.isOn = true;
        //}
        //else
        //{
        //    toggle.isOn = false;

        //}

    }


    public void DisplayRecruitCharacterList()
    {
        //Character生成



        //Character取得

        //CharacterManager.Instance.characterList.Countは仮状態
        //採用候補と従業員リストは分ける
        for (int i = 0; i < CharacterManager.Instance.candidateList.Count; i++)
        {
            GameObject original = CharacterManager.Instance.candidateList[i];

            //Characterデータセット
            //for文
            GameObject recruitChara = (GameObject)Instantiate(recruitCharacterPrefab);

            recruitChara.GetComponent<RecruitCharacterData>().SetCharacterData(original);
            recruitChara.transform.parent = charactersParent.transform;
            recruitList.Add(recruitChara);



        }

        //一人目を表示させる
        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(CharacterManager.Instance.candidateList[dispListNumber]);

    }


    public void OnClickLeftButton()
    {
        if (dispListNumber != 0)
            dispListNumber -= 1;
        else
            dispListNumber = recruitList.Count;//最大値

        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber].GetComponent<RecruitCharacterData>());
    }

    public void OnClickRightButton()
    {
        if (dispListNumber != recruitList.Count - 1)
            dispListNumber += 1;
        else
            dispListNumber = 0;

        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber].GetComponent<RecruitCharacterData>());
    }

    public void OnClickSelectToggle()
    {

        //recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected = true;

        //if (selectChara[0] == null)
        //{
        //    selectChara[0] = recruitList[dispListNumber].GetComponent<RecruitCharacterData>().GetOriginal;
        //}
        //else if (selectChara[1] == null)
        //{
        //    selectChara[1] = recruitList[dispListNumber].GetComponent<RecruitCharacterData>().GetOriginal;

        //}
        //else if(selectChara[0] == recruitList[dispListNumber].GetComponent<RecruitCharacterData>().GetOriginal)
        //{

        //}

        //recruitList[dispListNumber].GetComponent<RecruitCharacterData>().button;
        //bool set = recruitList[dispListNumber].GetComponent<RecruitCharacterData>().toggle.isOn;
        //recruitList[dispListNumber].GetComponent<RecruitCharacterData>().toggle.isOn != set ;
        bool set = recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected;
        recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected = !set;

        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber].GetComponent<RecruitCharacterData>());

    }

    //public void OnClickBackButton()
    //{
    //    displayCharacter.GetComponent<RecruitCharacterData>().selected = false;
    //}

    public void SelectCharacter()
    {
        //if(selectChara[0] == null)
        //{
        //    selectChara[0] = recruitList[dispListNumber].GetComponent<RecruitCharacterData>().GetOriginal;
        //}


    }
}
