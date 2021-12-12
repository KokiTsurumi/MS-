using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���\��Ui�@�N���X
/// <para>    �E�L�����N�^�[�A�E�Z���̐��e�L�X�g�A�����x�����N�p�����[�^ </para>
/// </summary>
public class InformationPop : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Slider pollutionSlider;


    void Start()
    {
        //�򉻓x�̍ő�l�̐ݒ�
        pollutionSlider.maxValue = 100;
        pollutionSlider.value = 0;
    }

    void Update()
    {
        
    }

    public void Create(string text, int pollutionLevel)
    {
        this.text.text = text;
        pollutionSlider.value = pollutionLevel;
    }

    public void OnClickClose()
    {
        //this.gameObject.SetActive(false);

        if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Information)
        {
            TutorialManager.Instance.NextStep();
        }
        else
        {
            Camera.main.GetComponent<CameraController>().ActionEnd();
        }

        Name_Value.Instance.PlusInfoCount();
        Name_Value.Instance.RankConfirm();
        RankUpUI.Instance.RankUpCheck();

        Destroy(this.gameObject);

    }
}
