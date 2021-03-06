using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeaDizolve : MonoBehaviour
{
    [SerializeField] GameObject cleanedUIPrefab;

    bool start = false;

    [SerializeField]
    float speed = 01.0f;

    float totalTime = 0;


    [SerializeField]
    Texture dissolvTexture;

    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        rend.material.shader = Shader.Find("Custom/DissolveShader");
        rend.material.SetTexture("_DissolveTex", dissolvTexture);
    }

    void Update()
    {
        if (!start) return;

        totalTime += Time.deltaTime * speed;

        if (totalTime >= 1)
            DisplayCleanedUI();

        rend.material.SetFloat("_Threshold", totalTime);

    }

    void DisplayCleanedUI()
    {
        if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Cleanning)
            TutorialManager.Instance.NextStep();

        Destroy(this.gameObject);


        if(IslandManager.Instance.totalPollutionLevel <= 0)
        {
            FeedBackCanvas.Instance.Feedback();
            return;
        }


        GameObject island = transform.root.gameObject;
        Instantiate(cleanedUIPrefab).GetComponent<CleanedUI>().Create(island);
    }

    public void DissolveStart()
    {
        //一度だけしか実行させない
        if (start) return;//すでにtrueであれば何もしない

        if (RankUpUI.Instance.useCanvas == true) return;

        if (Camera.main.GetComponent<CameraController>().GetTransState != CameraController.TransState.CENTER) return;

        start = true;
        RankUpUI.Instance.useCanvas = true;
        Camera.main.GetComponent<CameraController>().SetAction(false);
        //Camera.main.GetComponent<CameraController>().SetTransState(CameraController.TransState.CENTER);

    }
}
