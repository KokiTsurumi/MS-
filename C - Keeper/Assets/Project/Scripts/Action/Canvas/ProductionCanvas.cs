using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 生産　UI　クラス
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

        //アニメ終了時
        if (startAnimationCanvas.GetComponent<AnimationCallBack>().GetCallBack == true)
        {
            endToggle = true;
            startAnimationCanvas.SetActive(false);


            //RobotData data = originalRobot.GetComponent<RobotData>();

            //setRobot.SetData(data.name, data.clean, data.battery, data.specialSkill, null);

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
     
        }
    }


    public override void StartButton()
    {

        mainCanvas.SetActive(false);
        cameraController.backButton.SetActive(false);


        //研究値から生産できるロボットを計算
        CharacterManager.Instance.selectedCharacter[0] = selectChara[0].GetComponent<SelectCharacterDataInterface>().originalGameObject;
        CharacterManager.Instance.selectedCharacter[1] = selectChara[1].GetComponent<SelectCharacterDataInterface>().originalGameObject;

 

        startAnimationCanvas.SetActive(true);
    }


    public void TutorialProductionStart()
    {
        //CameraControllerの　action を falseにする
        GameObject obj = GameObject.Find("Production");
        obj.GetComponent<Production>().TutorialSetCanvas(this.gameObject);
        obj.GetComponent<Production>().ActionEnd();
    }
    
}
