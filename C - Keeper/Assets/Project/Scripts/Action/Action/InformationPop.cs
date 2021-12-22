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

    [SerializeField] Image resident;

    [SerializeField] Sprite[] residetSprites = new Sprite[2];

    class JsonInformationText
    {
        public string[] InformationBeforeList;
        public string[] InformationAfterList;
    }

    void Start()
    {
        //�򉻓x�̍ő�l�̐ݒ�
        //pollutionSlider.maxValue = 100;
        //pollutionSlider.value = 0;
    }

    void Update()
    {
        
    }

    public void Create(GameObject island)
    {

        string str = Resources.Load<TextAsset>("jsonFiles/jsonInformationList").ToString();
        JsonInformationText jsonText = JsonUtility.FromJson<JsonInformationText>(str);

        if (island.GetComponent<IslandBase>().GetPollutionLevel() > 0)
        {
            switch (island.gameObject.name)
            {
                case "Island_center":
                    this.text.text = jsonText.InformationBeforeList[0];
                    break;
                case "Island_2":
                    this.text.text = jsonText.InformationBeforeList[1];
                    break;
                case "Island_3":
                    this.text.text = jsonText.InformationBeforeList[2];
                    break;
                case "Island_4":
                    this.text.text = jsonText.InformationBeforeList[3];
                    break;
                case "Island_5":
                    this.text.text = jsonText.InformationBeforeList[4];
                    break;
                default:
                    text.text = "�I�u�W�F�N�g��������܂���ł���";
                    break;
            }

            resident.sprite = residetSprites[0];
        }
        else
        {
            switch (island.gameObject.name)
            {
                case "Island_center":
                    this.text.text = jsonText.InformationAfterList[0];
                    break;
                case "Island_2":
                    this.text.text = jsonText.InformationAfterList[1];
                    break;
                case "Island_3":
                    this.text.text = jsonText.InformationAfterList[2];
                    break;
                case "Island_4":
                    this.text.text = jsonText.InformationAfterList[3];
                    break;
                case "Island_5":
                    this.text.text = jsonText.InformationAfterList[4];
                    break;
                default:
                    text.text = "�I�u�W�F�N�g��������܂���ł���";
                    break;
            }

            resident.sprite = residetSprites[1];
            resident.rectTransform.sizeDelta = new Vector2(2048,1980);

        }



        pollutionSlider.value = island.GetComponent<IslandBase>().GetPollutionLevel();
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
        //Name_Value.Instance.RankConfirm();
        //RankUpUI.Instance.RankUpCheck();

        Destroy(this.gameObject);

    }
}
