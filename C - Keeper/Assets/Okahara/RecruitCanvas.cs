using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitCanvas : MonoBehaviour
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
    GameObject createCharacterCanvas;

    GameObject[] selectChara = new GameObject[2];

    

    [SerializeField]
    Toggle toggle;

    void Start()
    {
        //test
        createCharacterCanvas.SetActive(false);
    }

    void Update()
    {

       
    }


    public void DisplayRecruitCharacterList()
    {
        //Character����
        //Character�擾

        //�̗p���Ə]�ƈ����X�g�͕�����
        for (int i = 0; i < CharacterManager.Instance.candidateList.Count; i++)
        {
            GameObject original = CharacterManager.Instance.candidateList[i];

            //Character�f�[�^�Z�b�g
            //for��
            GameObject recruitChara = (GameObject)Instantiate(recruitCharacterPrefab);
            recruitChara.GetComponent<RecruitCharacterData>().SetCharacterData(original);
            recruitChara.transform.SetParent(charactersParent.transform,false);

            recruitList.Add(recruitChara);

        }

        //��l�ڂ�\��������
        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(CharacterManager.Instance.candidateList[dispListNumber]);

    }


    public void OnClickLeftButton()
    {
        recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected = toggle.isOn;
        

        if (dispListNumber != 0)
            dispListNumber -= 1;
        else
            dispListNumber = recruitList.Count - 1;//�ő�l

        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber].GetComponent<RecruitCharacterData>());
        toggle.isOn = recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected;
    }

    public void OnClickRightButton()
    {
        recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected = toggle.isOn;


        if (dispListNumber != recruitList.Count - 1)
            dispListNumber += 1;
        else
            dispListNumber = 0;

        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber].GetComponent<RecruitCharacterData>());
        toggle.isOn = recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected;

    }

    public void OnClickSelectToggle()
    {

        bool set = toggle.isOn;
        recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected = set;
        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber].GetComponent<RecruitCharacterData>());


        int cnt = 0;


        foreach (GameObject obj in recruitList)
        {
            if (obj.GetComponent<RecruitCharacterData>().selected == true)
            {
                selectChara[cnt] = obj.GetComponent<RecruitCharacterData>().original;
                cnt++;
            }

        }

        if (cnt >= 2)
        {
            
            createCharacterCanvas.SetActive(true);
            this.gameObject.SetActive(false);


            //�L�����N�^�[�f�[�^���Z�b�g
            createCharacterCanvas.transform.GetChild(1).GetComponent<RecruitCharacterData>().SetCharacterData(selectChara[0]);
            createCharacterCanvas.transform.GetChild(2).GetComponent<RecruitCharacterData>().SetCharacterData(selectChara[1]);

            return;
            //selectCanvas.SetActive(true);
        }
    }

    public void OnClickBackButton()
    {

        createCharacterCanvas.SetActive(false);
        this.gameObject.SetActive(true);

        foreach(GameObject obj in recruitList)
        {
            obj.GetComponent<RecruitCharacterData>().selected = false;
        }

        toggle.isOn = false;
    }

    public void OnClickStartButton()
    {
        this.gameObject.SetActive(false);
        createCharacterCanvas.SetActive(false);


        CharacterManager.Instance.HireCharacter(
            selectChara[0],
            selectChara[1]);


        //���b���Ԃ�u������������
        TutorialManager.Instance.NextStep();

    }
}
