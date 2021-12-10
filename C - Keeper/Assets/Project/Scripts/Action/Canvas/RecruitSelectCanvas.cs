using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitSelectCanvas : MonoBehaviour
{


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
    GameObject mainCanvas;

    //GameObject[] selectChara = new GameObject[2];

    //int[] selectNumber = new int[2];

    public GameObject GetDisplayCharacter => recruitList[dispListNumber];


    [SerializeField]
    Button button;
    //Toggle toggle;

    void Start()
    {
        //test
        //createCharacterCanvas.SetActive(false);
        Debug.Log("Recruit Select Canvas Start");


        Debug.Log(CharacterManager.Instance.candidateList.Count);
        for (int i = 0; i < CharacterManager.Instance.candidateList.Count; i++)
        {
            //GameObject original = CharacterManager.Instance.candidateList[i];
            Debug.Log(CharacterManager.Instance.candidateList[i].GetComponent<CharacterData>().name);
            Debug.Log(CharacterManager.Instance.candidateList[i].GetComponent<CharacterData>().research);
            Debug.Log(CharacterManager.Instance.candidateList[i].GetComponent<CharacterData>().production);
            Debug.Log(CharacterManager.Instance.candidateList[i].GetComponent<CharacterData>().management);
            Debug.Log(CharacterManager.Instance.candidateList[i].GetComponent<CharacterData>().introduction);
            //Characterデータセット
            GameObject recruitChara = (GameObject)Instantiate(recruitCharacterPrefab);
            recruitChara.GetComponent<RecruitCharacterData>().Create(CharacterManager.Instance.candidateList[i]);

            recruitChara.transform.SetParent(charactersParent.transform, false);

            recruitList.Add(recruitChara);

            
            
        }

        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber].GetComponent<RecruitCharacterData>());


        this.gameObject.SetActive(false);
    }

    void Update()
    {


    }


    //public void DisplayRecruitCharacterList()
    //{
    //    //Character生成
    //    //Character取得

    //    //採用候補と従業員リストは分ける
    //    //for (int i = 0; i < CharacterManager.Instance.candidateList.Count; i++)
    //    //{
    //    //    GameObject original = CharacterManager.Instance.candidateList[i];

    //    //    //Characterデータセット
    //    //    //for文
    //    //    GameObject recruitChara = (GameObject)Instantiate(recruitCharacterPrefab);
    //    //    recruitChara.GetComponent<RecruitCharacterData>().SetCharacterData(original);
    //    //    recruitChara.transform.SetParent(charactersParent.transform, false);

    //    //    recruitList.Add(recruitChara);

    //    //}

    //    //foreach(GameObject obj in recruitList)
    //    //{
    //    //    obj.GetComponent<RecruitCharacterData>().selected = false;
    //    //}

    //    //if()

        
    //    displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber].GetComponent<RecruitCharacterData>());

    //}


    public void OnClickLeftButton()
    {
        //recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected = button.enabled;


        if (dispListNumber != 0)
            dispListNumber -= 1;
        else
            dispListNumber = recruitList.Count - 1;//最大値

        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber].GetComponent<RecruitCharacterData>());
        button.enabled= ! recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected;
    }

    public void OnClickRightButton()
    {
        //recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected = toggle.isOn;


        if (dispListNumber != recruitList.Count - 1)
            dispListNumber += 1;
        else
            dispListNumber = 0;

        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber].GetComponent<RecruitCharacterData>());
        button.enabled = ! recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected;

    }

    public void OnClickSelectButton()
    {

        //bool set = toggle.isOn;



        recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected = true;

        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber].GetComponent<RecruitCharacterData>());

        button.enabled = false;
        //int cnt = 0;


        //foreach (GameObject obj in recruitList)
        //{
        //    if (obj.GetComponent<RecruitCharacterData>().selected == true)
        //    {
        //        selectChara[cnt] = obj.GetComponent<RecruitCharacterData>().original;
        //        cnt++;
        //    }

        //}

        //if (cnt >= 2)
        //{

        //    createCharacterCanvas.SetActive(true);
        //    this.gameObject.SetActive(false);


        //    //キャラクターデータをセット
        //    createCharacterCanvas.transform.GetChild(1).GetComponent<RecruitCharacterData>().SetCharacterData(selectChara[0]);
        //    createCharacterCanvas.transform.GetChild(2).GetComponent<RecruitCharacterData>().SetCharacterData(selectChara[1]);

        //    return;
        //    //selectCanvas.SetActive(true);
        //}

        this.gameObject.SetActive(false);
    }

    public void OnClickBackButton()
    {

        //createCharacterCanvas.SetActive(false);
        //this.gameObject.SetActive(true);

        //foreach (GameObject obj in recruitList)
        //{
        //    obj.GetComponent<RecruitCharacterData>().selected = false;
        //}

        //toggle.isOn = false;

        this.gameObject.SetActive(false);
    }

    //public void OnClickStartButton()
    //{
    //    this.gameObject.SetActive(false);
    //    createCharacterCanvas.SetActive(false);


    //    CharacterManager.Instance.HireCharacter(
    //        selectChara[0],
    //        selectChara[1]);


    //    //数秒時間を置いた方がいい
    //    TutorialManager.Instance.NextStep();

    //}
}
