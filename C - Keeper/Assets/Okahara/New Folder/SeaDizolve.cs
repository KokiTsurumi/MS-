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
    Texture mainTexture;

    [SerializeField]
    Texture dissolvTexture;

    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        rend.material.shader = Shader.Find("Custom/DissolveShader");
        //rend.material.SetTexture("_MainTex", mainTexture);
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
        Destroy(this.gameObject);
        GameObject island = transform.root.gameObject;
        Instantiate(cleanedUIPrefab).GetComponent<CleanedUI>().Create(island);
    }

    public void DissolveStart()
    {
        start = true;
        RankUpUI.Instance.pollutionRecruitCanvas = true;

    }
}
