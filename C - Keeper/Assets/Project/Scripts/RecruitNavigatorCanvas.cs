using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitNavigatorCanvas : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] AudioSource sound;

    [SerializeField] GameObject mainCanvas;

    bool start = false;

    //// Start is called before the first frame update
    void Start()
    {
        text.GetComponent<Text>().text = "���̓��̏Z�l���A�c�̂ɐ���Q���������ƌ����Ă��Ă��܂��I";
        sound.Play();
    }

    //// Update is called once per frame
    void Update()
    {
        if (text.GetComponent<TextFader>().enabled == false)
        {
            sound.Stop();
        }
    }

    public void RecruitEnd()
    {
        if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Recruit)
            TutorialCursor.Instance.SetActive(false);

        text.GetComponent<TextFader>().enabled = true;
        this.gameObject.SetActive(true);

        sound.Play();
        start = true;
    }

    public void OnClickNavigator()
    {
        if(text.GetComponent<TextFader>().enabled == true)
        {
            sound.Stop();
            text.GetComponent<TextFader>().enabled = false;
            return;
        }
        

        this.gameObject.SetActive(false);


        

        if (start == false)
        {
            start = true;
            text.GetComponent<Text>().text = "����ł܂��ꏏ�ɓ�����l�������܂����ˁI�I���������Y��ɂ��Ă����܂��傤�I";

            if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Recruit)
            {
                Debug.Log("Recruit");
                TutorialCursor.Instance.SetActive(true);
                TutorialCursor.Instance.SetPosition(TutorialCursor.CursorPositionList.characterSelectSlotelft);
            }


            mainCanvas.SetActive(true);   
        }
        else
        {
            if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Recruit)
            {
                TutorialManager.Instance.NextStep();
            }
            else if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.RankUpRecruit)
            {
                TutorialManager.Instance.NextStep();
            }

            GameObject recruitCanvas = transform.root.gameObject;

            Name_Value.Instance.PlusPlacementCountt();
            RankUpUI.Instance.useCanvas = false;

            Destroy(recruitCanvas);
        }
   
    }
}
