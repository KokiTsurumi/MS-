using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject listCanvas;
    [SerializeField] seni sceneChange;

    public AudioClip sound1;
    AudioSource audioSource;

    void Start()
    {
        menuButton.SetActive(true);
        listCanvas.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.No)
        //    menuButton.GetComponent<Image>().enabled = false;
        //else
        //    menuButton.GetComponent<Image>().enabled = true;

    }

    public void OnClickMenuButton()
    {
        menuButton.SetActive(false);
        listCanvas.SetActive(true);
        audioSource.PlayOneShot(sound1);
        //�Q�[�������Ԓ�~
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
    }

    public void OnClickEndGameButton()
    {
        
    }

    public void OnClickTitleButton()
    {
        //�f�[�^���ׂď�����������A�͂͂͂�
        sceneChange.FadeOut();
    }
}
