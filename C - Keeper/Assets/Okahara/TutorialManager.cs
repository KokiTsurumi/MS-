using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject navigatorText;
    

    List<string> textList;

    int listNumber = 0;


    [SerializeField]
    GameObject recruitCanvas;

    [SerializeField]
    GameObject tutorialCanvas;


    void Start()
    {
        textList = new List<string>();
        //�e�L�X�g�f�[�^��ǂݍ��߂�悤�ɂ���
        //�Ȃ����Ⴄ������������o�O���C������
        textList.Add("����1\n����1");
        textList.Add("����2\n");
        textList.Add("����3\n");
        textList.Add("����4\n");



        tutorialCanvas.SetActive(false);


        //�ŏ��ɑI������l�ނ𐶐�
        CharacterManager.Instance.CreateCandidateCharacter();

        StartCoroutine(Coroutine());        
    }

    // Update is called once per frame
    void Update()
    {
        navigatorText.GetComponent<Text>().text = textList[listNumber];
    }

    //�ŏ��Ɍق��L�����N�^�[��\��������
    public void RecruitCharacterDisplay()
    {
        //�L�����o�XSetActive true
    }

    public void OnClickNextText()
    {
        listNumber += 1;
        navigatorText.GetComponent<Text>().text = textList[listNumber];
        navigatorText.GetComponent<TextFader>().enabled = true;
    }

    IEnumerator Coroutine()
    {

        yield return new WaitForSeconds(0.5f);

        recruitCanvas.GetComponent<RecruitCanvas>().DisplayRecruitCharacterList();
    }
}
