using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���\��Ui�@�N���X
/// <para>    �E�L�����N�^�[�A�E�Z���̐��e�L�X�g�A�����x�����N�p�����[�^ </para>
/// </summary>
public class InformationPop : MonoBehaviour
{
    void Start()
    {�@
        
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
