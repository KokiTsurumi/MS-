using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Name_Value : MonoBehaviour
{

    public Image image;
    private Sprite sprite;

    public int infoCount, placementCount, researchCount, cleaningCount, productionCount;    //各項目のカウント

    // Use this for initialization
    void Start()
    {
        /*=========================  ランク1  ===========================*/
        sprite = Resources.Load<Sprite>("character1");
        image = this.GetComponent<Image>();
        image.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        /*=========================  ランク2  ===========================*/
        //すべての項目を一通り終了した(チュートリアルが完了した)時
        if ((infoCount ==1) && (placementCount == 1) && (researchCount == 1) && (cleaningCount == 1) && (productionCount == 1))
        {
            sprite = Resources.Load<Sprite>("character2");
            image = this.GetComponent<Image>();
            image.sprite = sprite;
        }

        /*=========================  ランク3  ===========================*/
        if ((infoCount == 2) && (placementCount == 2) && (researchCount == 2) && (cleaningCount == 2) && (productionCount == 2))
        {
            sprite = Resources.Load<Sprite>("character3");
            image = this.GetComponent<Image>();
            image.sprite = sprite;
        }

        /*=========================  ランク4  ===========================*/
        if ((infoCount == 3) && (placementCount == 3) && (researchCount == 3) && (cleaningCount == 3) && (productionCount == 3))
        {
            sprite = Resources.Load<Sprite>("character4");
            image = this.GetComponent<Image>();
            image.sprite = sprite;
        }
        /*=========================  ランク5  ===========================*/
        if ((infoCount == 4) && (placementCount == 4) && (researchCount == 4) && (cleaningCount == 4) && (productionCount == 4))
        {
            sprite = Resources.Load<Sprite>("character5");
            image = this.GetComponent<Image>();
            image.sprite = sprite;
        }
    }
}