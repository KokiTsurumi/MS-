using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 情報表示Ui　クラス
/// <para>    ・キャラクター、・住民の声テキスト、汚染度ランクパラメータ </para>
/// </summary>
public class InformationPop : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Slider pollutionSlider;


    void Start()
    {
        //浄化度の最大値の設定
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
