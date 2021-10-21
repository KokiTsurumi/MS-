using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public Text scoreText;

    public GameObject panelManager;

    private int index = 0;

    void Start()
    {
        panelManager = GameObject.Find("PanelManager");
    }

    void Update()
    {
        index = panelManager.GetComponent<PanelManager>().GetIndex();
        scoreText.text = "�V�l���̗p�@" + index.ToString() + "/5";    // �X�R�A�\��
    }
}