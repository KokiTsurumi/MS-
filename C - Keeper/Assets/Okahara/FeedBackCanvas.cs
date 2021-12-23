using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FeedBackCanvas : SingletonMonoBehaviour<FeedBackCanvas>
{
    // Start is called before the first frame update
    [SerializeField] Image commentImage;

    [SerializeField] Sprite[] feedBackSprites = new Sprite[6];

    [SerializeField] Image face;

    [SerializeField] Sprite sadFace;

    [SerializeField] seni sceneChange;

    

    Animator animator;

    bool end = false;

    bool allFinish = false;

    void Start()
    {
        //this.gameObject.SetActive(false);
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!end) return;


        if(!allFinish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("End", true);
            }

        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                sceneChange.FadeOut();
            }
        }

    }

    public void Feedback()
    {
        StartCoroutine(CoroutineTimer());
    }

    IEnumerator CoroutineTimer()
    {
        yield return new WaitForSeconds(2.0f);

        this.gameObject.SetActive(true);

        animator.SetBool("Start", true);

        string rank = WorldManager.Instance.EvaluateClearTime();

        switch (rank)
        {
            case "S":
                commentImage.sprite = feedBackSprites[0];
                animator.SetBool("S", true);
                break;
            case "A":
                commentImage.sprite = feedBackSprites[1];
                animator.SetBool("A", true);
                break;
            case "B":
                commentImage.sprite = feedBackSprites[2];
                animator.SetBool("B", true);
                break;
            case "C":
                commentImage.sprite = feedBackSprites[3];
                animator.SetBool("C", true);
                break;
            case "D":
                commentImage.sprite = feedBackSprites[4];
                animator.SetBool("D", true);
                face.sprite = sadFace;
                break;
            case "E":
                commentImage.sprite = feedBackSprites[5];
                animator.SetBool("E", true);
                face.sprite = sadFace;
                break;
        }
    }

    public void AnimationEnd()
    {
        end = true;
    }

    public void ThankyouEnd()
    {
        allFinish = true;
    }
}
