using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplanationCanvas : MonoBehaviour
{
    [SerializeField] GameObject explanationBackButton;

    [SerializeField] Image explanationImage;

    [SerializeField] Sprite[] sprites = new Sprite[8];

    [SerializeField] GameObject backButton;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;

    int number = 0;

    private void Start()
    {
        explanationImage.enabled = false;
        backButton.SetActive(false);
        rightButton.SetActive(false);
        leftButton.SetActive(false);
        explanationBackButton.SetActive(true);
    }

    void ExplanationButton()
    {
        backButton.SetActive(true);
        rightButton.SetActive(true);
        leftButton.SetActive(true);
        explanationBackButton.SetActive(false);

        explanationImage.enabled = true;
        explanationImage.sprite = sprites[number];
    }

    public void OnClickBackButton()
    {
        explanationImage.enabled = false;
        backButton.SetActive(false);
        rightButton.SetActive(false);
        leftButton.SetActive(false);
        explanationBackButton.SetActive(true);

    }

    public void OnClickButton_0()
    {
        number = 0;
        ExplanationButton();
    }

    public void OnClickButton_1()
    {
        number = 1;
        ExplanationButton();

    }

    public void OnClickButton_2()
    {
        number = 2;
        ExplanationButton();

    }

    public void OnClickButton_3()
    {
        number = 3;
        ExplanationButton();

    }

    public void OnClickButton_4()
    {
        number = 4;
        ExplanationButton();

    }

    public void OnClickButton_5()
    {
        number = 5;
        ExplanationButton();

    }

    public void OnClickButton_6()
    {
        number = 6;
        ExplanationButton();

    }

    public void OnClickButton_7()
    {
        number = 7;
        ExplanationButton();

    }

    public void OnClickLeftButton()
    {
        if (number != 0)
            number -= 1;
        else
            number = 7;

        explanationImage.sprite = sprites[number];
    }

    public void OnClickRightButton()
    {
        if (number != 7)
            number += 1;
        else
            number = 0;

        explanationImage.sprite = sprites[number];
    }

    
}
