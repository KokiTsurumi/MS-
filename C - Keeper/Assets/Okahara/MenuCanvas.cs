using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject listCanvas;
    [SerializeField] seni sceneChange;

    void Start()
    {
        menuButton.SetActive(true);
        listCanvas.SetActive(false);
    }

    void Update()
    {
        
    }

    public void OnClickMenuButton()
    {
        menuButton.SetActive(false);
        listCanvas.SetActive(true);

        //�Q�[�������Ԓ�~
    }

    public void OnClickConfigButton()
    {

    }

    public void OnClickExplanationButton()
    {

    }

    public void OnClickBackButton()
    {
        menuButton.SetActive(true);
        listCanvas.SetActive(false);
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
