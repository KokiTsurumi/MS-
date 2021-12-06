using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProductionCanvas : SelectCanvasInterface
{
    [SerializeField]
    GameObject RobotCanvas;

    //[SerializeField]
    //GameObject robotPrefab;


    [SerializeField]
    CreateRobotData setRobot;

    bool endToggle = false;

    
    GameObject originalRobot;

    private void Start()
    {
        RobotCanvas.SetActive(false);
    }


    private void Update()
    {
        if (endToggle) return;

        //アニメ終了時
        if (startAnimationCanvas.GetComponent<AnimationCallBack>().GetCallBack == true)
        {
            endToggle = true;
            startAnimationCanvas.SetActive(false);


            RobotData data = originalRobot.GetComponent<RobotData>();

            setRobot.SetData(data.name, data.clean, data.battery, data.specialSkill, null);


            if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Production)
            {
                //タイマー計算処理
                //target.GetComponent<IslandBase>().StartInvestigate(0);//固定?

                RobotCanvas.SetActive(true);

            }
            else
            {

                //float time = RobotManager.Instance.CalcCleanTime(data, data);
                //タイマー計算処理
                //target.GetComponent<IslandBase>().start(time);

            }

        }
    }



    public override void StartButton()
    {

        mainCanvas.SetActive(false);
        cameraController.backButton.SetActive(false);

        //RobotCanvas.SetActive(true);

        //研究値から生産できるロボットを計算
        CharacterManager.Instance.selectedCharacter[0] = selectChara[0].GetComponent<SelectCharacterDataInterface>().data.originalGameObject;
        CharacterManager.Instance.selectedCharacter[1] = selectChara[1].GetComponent<SelectCharacterDataInterface>().data.originalGameObject;

        //ロボットを生成
        originalRobot = RobotManager.Instance.GenerateRobot();
        

        startAnimationCanvas.SetActive(true);
    }



    public void CloseRobotCanvas()
    {
        //base.StartButton();

        //RobotCanvas.SetActive(false);

        //Destroy(this.gameObject);

        if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Production)
        {
            Destroy(this.gameObject);
        }
        else
        {
            cameraController.ActionEnd();

        }

    }
}
