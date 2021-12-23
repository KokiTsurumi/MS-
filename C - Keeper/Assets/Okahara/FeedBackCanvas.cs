using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedBackCanvas : SingletonMonoBehaviour<FeedBackCanvas>
{
    // Start is called before the first frame update
    [SerializeField] Image commentImage;

    [SerializeField] Sprite[] feedBackSprites = new Sprite[6];

    [SerializeField] Image face;

    [SerializeField] Sprite sadFace;

    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Feedback()
    {
        StartCoroutine(CoroutineTimer());
    }

    IEnumerator CoroutineTimer()
    {
        yield return new WaitForSeconds(2.0f);

        this.gameObject.SetActive(true);


        string rank = WorldManager.Instance.EvaluateClearTime();

        switch (rank)
        {
            case "S":
                commentImage.sprite = feedBackSprites[0];
                break;
            case "A":
                commentImage.sprite = feedBackSprites[1];
                break;
            case "B":
                commentImage.sprite = feedBackSprites[2];
                break;
            case "C":
                commentImage.sprite = feedBackSprites[3];
                break;
            case "D":
                commentImage.sprite = feedBackSprites[4];
                face.sprite = sadFace;
                break;
            case "E":
                commentImage.sprite = feedBackSprites[5];
                face.sprite = sadFace;
                break;
        }
    }
}
