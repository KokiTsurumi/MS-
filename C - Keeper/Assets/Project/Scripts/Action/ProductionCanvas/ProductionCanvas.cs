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

            GameObject island = IslandManager.Instance.GetCurrentIsland();

            if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Production)
            {
                //�^�C�}�[�v�Z����
                island.GetComponent<IslandBase>().StartProduction(0, CreateRobotCanvas);


            }
            else
            {

                //�^�C�}�[�v�Z����
                float time = CharacterManager.Instance.CalcProductionTime();
                island.GetComponent<IslandBase>().StartProduction(time, CreateRobotCanvas);

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
        
        if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Production)
        {
            Debug.Log("productionEnd");
            TutorialManager.Instance.NextStep();
            Destroy(this.gameObject);
        }
        else
        {
            cameraController.ActionEnd();

        }

    }

    public void CreateRobotCanvas()
    {
        IslandManager.Instance.GetCurrentIsland().GetComponent<IslandBase>().timer.SetActive(false);
        RobotCanvas.SetActive(true);
    }
}
