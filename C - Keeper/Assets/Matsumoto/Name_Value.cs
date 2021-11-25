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

    public int infoCount, placementCount, researchCount, cleaningCount, productionCount;    //�e���ڂ̃J�E���g

    // Use this for initialization
    void Start()
    {
        /*=========================  �����N1  ===========================*/
        //sprite = Resources.Load<Sprite>("character1");
        //image = this.GetComponent<Image>();
        //image.sprite = rankSprite[0];
        numberImage.sprite = numberSprite[0];
        frameImage.sprite = frameSprite[0];

    }

    // Update is called once per frame
    void Update()
    {
        /*=========================  �����N2  ===========================*/
        //���ׂĂ̍��ڂ���ʂ�I������(�`���[�g���A������������)��
        if ((infoCount ==1) && (placementCount == 1) && (researchCount == 1) && (cleaningCount == 1) && (productionCount == 1))
        {
            //sprite = Resources.Load<Sprite>("character2");
            //image = this.GetComponent<Image>();
            numberImage.sprite = numberSprite[1];
        frameImage.sprite = frameSprite[1];

        }

        /*=========================  �����N3  ===========================*/
        if ((infoCount == 2) && (placementCount == 2) && (researchCount == 2) && (cleaningCount == 2) && (productionCount == 2))
        {
            //sprite = Resources.Load<Sprite>("character3");
            //image = this.GetComponent<Image>();
            //image.sprite = numberSprite[2];
            numberImage.sprite = numberSprite[2];
            frameImage.sprite = frameSprite[2];

        }

        /*=========================  �����N4  ===========================*/
        if ((infoCount == 3) && (placementCount == 3) && (researchCount == 3) && (cleaningCount == 3) && (productionCount == 3))
        {
            //sprite = Resources.Load<Sprite>("character4");
            //image = this.GetComponent<Image>();
            //image.sprite = numberSprite[3];
            numberImage.sprite = numberSprite[3];
            frameImage.sprite = frameSprite[3];

        }
        /*=========================  �����N5  ===========================*/
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