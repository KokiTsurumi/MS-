using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : SingletonMonoBehaviour<MenuCanvas>
{
    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject listCanvas;
    [SerializeField] GameObject explanationCanvas;
    [SerializeField] seni sceneChange;

    public AudioClip sound1;
    AudioSource audioSource;

    public bool menuButtonEnabled = true;
    public bool tutorialExplanation = false;

    bool timeStop = false;
    public bool GetTimeStop => timeStop;

    void Start()
    {
        menuButton.SetActive(true);
        listCanvas.SetActive(false);
        explanationCanvas.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    //public void 
  

    public void OnClickMenuButton()
    {
        if (!menuButtonEnabled) return;

        menuButton.SetActive(false);
        listCanvas.SetActive(true);
        audioSource.PlayOneShot(sound1);
        //ゲーム内時間停止
        WorldManager.Instance.TimeStop(true);

        timeStop = true;

        if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Menu)
        {
            //TutorialCursor.Instance.SetActive(false);
            TutorialCursor.Instance.SetScaleChange(1);
            TutorialCursor.Instance.SetPosition(TutorialCursor.CursorPositionList.menuExplanantion);
        }
    }

    public void OnClickConfigButton()
    {
        if (tutorialExplanation) return;
        audioSource.PlayOneShot(sound1);
    }

    public void OnClickExplanationButton()
    {
        audioSource.PlayOneShot(sound1);
        explanationCanvas.SetActive(true);

        if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Menu)
            TutorialCursor.Instance.SetActive(false);

        tutorialExplanation = false;
    }

    public void OnClickBackButton()
    {
        if (tutorialExplanation) return;

        menuButton.SetActive(true);
        listCanvas.SetActive(false);
        audioSource.PlayOneShot(sound1);

        //ゲーム内時間開始
        if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.No)
            WorldManager.Instance.TimeStop(false);

        

        timeStop = false;
    }

    public void OnClickEndGameButton()
    {
        if (tutorialExplanation) return;

        Application.Quit();
    }

    public void OnClickTitleButton()
    {
        if (tutorialExplanation) return;

        //データすべて初期化されるよ、はははは
        sceneChange.FadeOut();
    }

    public void OnClickCloseButton()
    {
        menuButton.SetActive(false);
        listCanvas.SetActive(true);
        explanationCanvas.SetActive(false);

        audioSource = GetComponent<AudioSource>();

        if (! tutorialExplanation)
            TutorialManager.Instance.NextStep();
    }
}
