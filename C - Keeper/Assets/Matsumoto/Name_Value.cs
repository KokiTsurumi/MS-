using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Name_Value : SingletonMonoBehaviour<Name_Value>
{
    //各項目の設定
    public int infoCount, placementCount, researchCount, cleaningCount, productionCount;
    public int myRank;

    //public Image[] image = new Image[2];
    [SerializeField] Image numberImage;
    [SerializeField] Image frameImage;
    //private Sprite sprite;

    [SerializeField]
    Sprite[] numberSprite = new Sprite[5];

    [SerializeField]
    Sprite[] frameSprite = new Sprite[5];

    [SerializeField] AudioClip finishSound;

    AudioSource audioSource;


    // 最初のランク設定
    void Start()
    {
        /*=========================  ランク1  ===========================*/
        //sprite = Resources.Load<Sprite>("character1");
        //image = this.GetComponent<Image>();
        //image.sprite = rankSprite[0];
        numberImage.sprite = numberSprite[0];
        frameImage.sprite = frameSprite[0];
        myRank = 1;

        audioSource = GetComponent<AudioSource>();
    }

    //情報を一通り終えた時に呼び出す関数
    public void PlusInfoCount()
    {
        infoCount += 1;
    }

    //人材配置を一通り終えた時に呼び出す関数
    public void PlusPlacementCountt()
    {
        placementCount += 1;
    }

    //調査を一通り終えた時に呼び出す関数
    public void PlusResearchCount()
    {
        researchCount += 1;


        audioSource.PlayOneShot(finishSound);
    }

    //清掃を一通り終えた時に呼び出す関数
    public void PlusCleaningCount()
    {
        cleaningCount += 1;
    }

    //生産を一通り終えた時に呼び出す関数
    public void PlusProductionCount()
    {
        productionCount += 1;

    }


    //ランクが上がるかどうか確かめる関数 
    public void RankConfirm()
    {
        /*=========================  ランク2になる時  ===========================*/
        //すべての項目を一通り終了した(チュートリアルが完了した)時
        if (myRank == 1)
        {
            
            if ((placementCount >= 1) && (researchCount >= 1) && (cleaningCount >= 1) && (productionCount >= 1))
            {
                //sprite = Resources.Load<Sprite>("character2");
                //image = this.GetComponent<Image>();
                numberImage.sprite = numberSprite[1];
                frameImage.sprite = frameSprite[1];
                myRank = 2;
            }
        }

        /*=========================  ランク3になる時  ===========================*/
        if (myRank == 2)
        {
            
            if ((cleaningCount >= 2) &&(researchCount >= 2) && (productionCount >= 2) && (placementCount >= 2))
            {
                //sprite = Resources.Load<Sprite>("character3");
                //image = this.GetComponent<Image>();
                //image.sprite = numberSprite[2];
                numberImage.sprite = numberSprite[2];
                frameImage.sprite = frameSprite[2];
                myRank = 3;

            }
        }

        /*=========================  ランク4になる時  ===========================*/
        if (myRank == 3)
        {
            
            if ((cleaningCount >= 4) && (productionCount >= 3))
            {
                //sprite = Resources.Load<Sprite>("character4");
                //image = this.GetComponent<Image>();
                //image.sprite = numberSprite[3];
                numberImage.sprite = numberSprite[3];
                frameImage.sprite = frameSprite[3];
                myRank = 4;

            }
        }

        /*=========================  ランク5になる時  ===========================*/
        if (myRank == 4)
        {
            
            if ((cleaningCount >= 6) && (researchCount >= 3))
            {
                //sprite = Resources.Load<Sprite>("character5");
                //image = this.GetComponent<Image>();
                //image.sprite = numberSprite[4];
                numberImage.sprite = numberSprite[4];
                frameImage.sprite = frameSprite[4];
                myRank = 5;
            }
        }
       
    }
    
}