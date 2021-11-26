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
        //テキストデータを読み込めるようにする
        //なぜか違う文字がちらつくバグも修正する
        textList.Add("文章1\n文章1");
        textList.Add("文章2\n");
        textList.Add("文章3\n");
        textList.Add("文章4\n");



        tutorialCanvas.SetActive(false);


        //最初に選択する人材を生成
        CharacterManager.Instance.CreateCandidateCharacter();

        StartCoroutine(Coroutine());        
    }

    // Update is called once per frame
    void Update()
    {
        navigatorText.GetComponent<Text>().text = textList[listNumber];
    }

    //最初に雇うキャラクターを表示させる
    public void RecruitCharacterDisplay()
    {
        //キャンバスSetActive true
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
