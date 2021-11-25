using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Name_Value : MonoBehaviour
{

    //public Image[] image = nsew Image[2];
    [SerializeField] Image numberImage;
    [SerializeField] Image frameImage;
    //private Sprite sprite;

    [SerializeField]
    Sprite[] numberSprite = new Sprite[5];

    [SerializeField]
    Sprite[] frameSprite = new Sprite[5];

    public int infoCount, placementCount, researchCount, cleaningCount, productionCount;    //各項目のカウント

    // Use this for initialization
    void Start()
    {
        /*=========================  ランク1  ===========================*/
        //sprite = Resources.Load<Sprite>("character1");
        //image = this.GetComponent<Image>();
        //image.sprite = rankSprite[0];
        numberImage.sprite = numberSprite[0];
        frameImage.sprite = frameSprite[0];

    }

    // Update is called once per frame
    void Update()
    {
        /*=========================  ランク2  ===========================*/
        //すべての項目を一通り終了した(チュートリアルが完了した)時
        if ((infoCount ==1) && (placementCount == 1) && (researchCount == 1) && (cleaningCount == 1) && (productionCount == 1))
        {
            //sprite = Resources.Load<Sprite>("character2");
            //image = this.GetComponent<Image>();
            numberImage.sprite = numberSprite[1];
        frameImage.sprite = frameSprite[1];

        }

        /*=========================  ランク3  ===========================*/
        if ((infoCount == 2) && (placementCount == 2) && (researchCount == 2) && (cleaningCount == 2) && (productionCount == 2))
        {
            //sprite = Resources.Load<Sprite>("character3");
            //image = this.GetComponent<Image>();
            //image.sprite = numberSprite[2];
            numberImage.sprite = numberSprite[2];
            frameImage.sprite = frameSprite[2];

        }

        /*=========================  ランク4  ===========================*/
        if ((infoCount == 3) && (placementCount == 3) && (researchCount == 3) && (cleaningCount == 3) && (productionCount == 3))
        {
            //sprite = Resources.Load<Sprite>("character4");
            //image = this.GetComponent<Image>();
            //image.sprite = numberSprite[3];
            numberImage.sprite = numberSprite[3];
            frameImage.sprite = frameSprite[3];

        }
        /*=========================  ランク5  ===========================*/
        if ((infoCount == 4) && (placementCount == 4) && (researchCount == 4) && (cleaningCount == 4) && (productionCount == 4))
        {
            //sprite = Resources.Load<Sprite>("character5");
            //image = this.GetComponent<Image>();
            //image.sprite = numberSprite[4];
            numberImage.sprite = numberSprite[4];
            frameImage.sprite = frameSprite[4];

        }
    }
}