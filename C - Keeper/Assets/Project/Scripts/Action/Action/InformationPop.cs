using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 情報表示Ui　クラス
/// <para>    ・キャラクター、・住民の声テキスト、汚染度ランクパラメータ </para>
/// </summary>
public class InformationPop : MonoBehaviour
{
    void Start()
    {　
        
    }

    void Update()
    {
        
    }

    public void OnClickClose()
    {
        //this.gameObject.SetActive(false);

        if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Information)
        {
            TutorialManager.Instance.NextStep();
        }


        Destroy(this.gameObject);
    }
}
