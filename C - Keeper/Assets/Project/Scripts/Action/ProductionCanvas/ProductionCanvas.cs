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

        //�A�j���I����
        if (startAnimationCanvas.GetComponent<AnimationCallBack>().GetCallBack == true)
        {
            endToggle = true;
            startAnimationCanvas.SetActive(false);


            RobotData data = originalRobot.GetComponent<RobotData>();

            setRobot.SetData(data.name, data.clean, data.battery, data.specialSkill, null);


            if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Production)
            {
                //�^�C�}�[�v�Z����
                //target.GetComponent<IslandBase>().StartInvestigate(0);//�Œ�?

                RobotCanvas.SetActive(true);

            }
            else
            {

                //float time = RobotManager.Instance.CalcCleanTime(data, data);
                //�^�C�}�[�v�Z����
                //target.GetComponent<IslandBase>().start(time);

            }

        }
    }



    public override void StartButton()
    {

        mainCanvas.SetActive(false);
        cameraController.backButton.SetActive(false);

        //RobotCanvas.SetActive(true);

        //�����l���琶�Y�ł��郍�{�b�g���v�Z
        CharacterManager.Instance.selectedCharacter[0] = selectChara[0].GetComponent<SelectCharacterDataInterface>().data.originalGameObject;
        CharacterManager.Instance.selectedCharacter[1] = selectChara[1].GetComponent<SelectCharacterDataInterface>().data.originalGameObject;

        //���{�b�g�𐶐�
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
