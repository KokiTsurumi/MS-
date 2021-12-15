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

        //ゲーム内時間停止
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
        //ゲーム内時間開始
    }

    public void OnClickEndGameButton()
    {

    }

    public void OnClickTitleButton()
    {
        //データすべて初期化されるよ、はははは
        sceneChange.FadeOut();
    }
}
