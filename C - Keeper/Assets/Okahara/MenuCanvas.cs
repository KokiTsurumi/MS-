using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : SingletonMonoBehaviour<MenuCanvas>
{
    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject listCanvas;
    [SerializeField] seni sceneChange;

    public AudioClip sound1;
    AudioSource audioSource;

    public bool menuButtonEnabled = true;

    void Start()
    {
        menuButton.SetActive(true);
        listCanvas.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    //public void 
  

    public void OnClickMenuButton()
    {
        if (!menuButtonEnabled) return;

        menuButton.SetActive(false);
        listCanvas.SetActive(true);
        audioSource.PlayOneShot(sound1);
        //�Q�[�������Ԓ�~
        WorldManager.Instance.TimeStop(true);

    }

    public void OnClickConfigButton()
    {
        audioSource.PlayOneShot(sound1);
    }

    public void OnClickExplanationButton()
    {
        audioSource.PlayOneShot(sound1);
    }

    public void OnClickBackButton()
    {
        menuButton.SetActive(true);
        listCanvas.SetActive(false);
        audioSource.PlayOneShot(sound1);

        //�Q�[�������ԊJ�n
        if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.No)
            WorldManager.Instance.TimeStop(false);

    }

    public void OnClickEndGameButton()
    {
        Application.Quit();
    }

    public void OnClickTitleButton()
    {
        //�f�[�^���ׂď�����������A�͂͂͂�
        sceneChange.FadeOut();
    }
}
