using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ���Y�@UI�@�N���X
/// </summary>
public class ProductionCanvas : SelectCanvasInterface
{

    //[SerializeField] CreateRobotData setRobot;

    bool endToggle = false;

    //GameObject originalRobot = null;

    public bool createStart = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (endToggle) return;

        //�A�j���I����
        if (startAnimationCanvas.GetComponent<AnimationCallBack>().GetCallBack == true)
        {
            endToggle = true;
            startAnimationCanvas.SetActive(false);

            createStart = true;

            if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Production)
            {

                TutorialManager.Instance.ProductionTimerSet(TutorialProductionStart);
               
            }
            else
            {
                cameraController.ActionEnd();
            }


            this.gameObject.SetActive(false);

            Camera.main.GetComponent<CameraController>().GetCenterIsland.GetComponent<IslandBase>().timer.GetComponent<Canvas>().enabled = true;

        }
        else if (tagAnimationCanvas.GetComponent<AnimationCallBack>().GetCallBack == true)
        {
            startAnimationCanvas.SetActive(true);
        }
    }


    public override void StartButton()
    {

        mainCanvas.SetActive(false);
        cameraController.backButton.SetActive(false);


        //�����l���琶�Y�ł��郍�{�b�g���v�Z
        CharacterManager.Instance.selectedCharacter[0] = selectChara[0].GetComponent<SelectCharacterDataInterface>().originalGameObject;
        CharacterManager.Instance.selectedCharacter[1] = selectChara[1].GetComponent<SelectCharacterDataInterface>().originalGameObject;

        //�^�b�O���o
        if (CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().tagName == CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().tagName
            &&
           CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().tagName != "�Ȃ�")
        {
            tagAnimationCanvas.SetActive(true);

        }
        else
        {
            startAnimationCanvas.SetActive(true);
        }

        TutorialCursor.Instance.SetActive(false);

    }


    public void TutorialProductionStart()
    {
        GameObject obj = GameObject.Find("Production");
        obj.GetComponent<Production>().TutorialSetCanvas(this.gameObject);
        obj.GetComponent<Production>().ActionEnd();
    }
    
}
