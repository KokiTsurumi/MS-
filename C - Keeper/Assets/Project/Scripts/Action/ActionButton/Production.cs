using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 生産ボタン　クラス
/// </summary>
public class Production : ActionButtonInterface
{
    [SerializeField] GameObject productionUIPrefab;
    [SerializeField] GameObject robotCanvas;
    [SerializeField] GameObject checkMarkIcon;

 
    [SerializeField]
    GameObject createRobot;

    //[SerializeField]
    //GameObject robotCanvas;

    [SerializeField]
    bool creating = false;
    [SerializeField]
    bool createEnd = false;

    //[SerializeField]
    //bool doing = false;
    [SerializeField] AudioClip finishSound;
    [SerializeField] AudioClip closeSound;

    AudioSource audioSource;

    bool watched = false;

    bool soundPlay = false;

    new private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        base.Start();
        //RobotCanvas.SetActive(false);
        checkMarkIcon.SetActive(false);
    }

    override public void ActionStart()
    {
        //doing = true;
        if (creating)
        {
            Camera.main.GetComponent<CameraController>().GetCenterIsland.GetComponent<IslandBase>().timer.GetComponent<Canvas>().enabled = true;
            cameraController.ActionEnd();
            return;
        }
        else if (createEnd)
        {
            //createRobotCanvas.SetActive(true);
            //checkMarkIcon.SetActive(true);

            return;
        }
        //{
        //    IslandManager.Instance.GetCurrentIsland().GetComponent<IslandBase>().timer.GetComponent<Canvas>().enabled = true;
        //}

        cameraController.backButton.SetActive(true);

        canvas = (GameObject)Instantiate(productionUIPrefab);
        canvas.transform.GetComponent<ProductionCanvas>().Initialize();
    }

    public override void ActionEnd()
    {
        //doing = false;
        cameraController.backButton.SetActive(false);

        if (creating)
        {
            cameraController.ZoomOut();
            return;
        }
        
        if(canvas != null)
        {
            if (canvas.GetComponent<ProductionCanvas>().createStart == true)
            {
                canvas.GetComponent<ProductionCanvas>().createStart = false;

                GameObject island = Camera.main.GetComponent<CameraController>().GetCenterIsland;

                if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Production)
                {
                    //タイマー計算処理
                    CreateRobotStart();
                    island.GetComponent<IslandBase>().StartProduction(2.0f, CreateRobotCanvas);
                }
                else
                {
                    //タイマー計算処理
                    float time = CharacterManager.Instance.CalcProductionTime();
                    island.GetComponent<IslandBase>().StartProduction(time, CreateRobotCanvas);
                    CreateRobotStart();


                    //cameraController.ActionEnd();
                    cameraController.ZoomOut();


                }

                //ロボットを生成
                createRobot = RobotManager.Instance.GenerateRobot();


                //時間を待たずして使用可能になってしまうため、
                //清掃ロボットリスト生成時に使用不可にできる条件分を加える
                createRobot.transform.position = new Vector3(1, 0, 0);


                CharacterManager.Instance.UseCharacter();



            }
            else
            {
                Destroy(canvas);
                cameraController.ZoomOut();

            }
        }
   

        //Destroy(canvas);
        //cameraController.ActionButtonRepop();
    }

    private void CreateRobotStart()
    {
        Camera.main.GetComponent<CameraController>().GetCenterIsland.GetComponent<IslandBase>().timer.SetActive(true);
        //IslandManager.Instance.GetCurrentIsland().GetComponent<IslandBase>().timer.GetComponent<Canvas>().enabled = true;   
        creating = true;
    }

    private void Update()
    {
        if (true)
        {
            if(createEnd && watched == false)
            {
                //チェック表示
                //createRobotCanvas.SetActive(true);

                if(TutorialManager.Instance.tutorialState != TutorialManager.TutorialState.No)
                {
                    if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Production)
                    {
                        TutorialManager.Instance.tutorialState = TutorialManager.TutorialState.ProductionRobotCreate;
                        Camera.main.GetComponent<CameraController>().GetCenterIsland.GetComponent<IslandBase>().timer.SetActive(false);
                        checkMarkIcon.SetActive(true);

                        if (soundPlay == false)
                        {
                            audioSource.PlayOneShot(finishSound);
                            soundPlay = true;
                        }
                        TutorialManager.Instance.NextStep();
                    }
                }
                else
                {
                    checkMarkIcon.SetActive(true);
                    if (soundPlay == false)
                    {
                        audioSource.PlayOneShot(finishSound);
                        soundPlay = true;
                    }


                    Camera.main.GetComponent<CameraController>().GetCenterIsland.GetComponent<IslandBase>().timer.SetActive(false);
                }



            }
        }
    }

    //public void CreateStart()
    //{
    //    createStart = true;
    //}

    //public void CloseRobotCanvas()
    //{

    //    if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Production)
    //    {
    //        //RobotCanvas.SetActive(false);

    //        StartCoroutine(TutorialCloseRobotCanvas());

    //    }
    //    else
    //    {
    //    }

    //}

    public void TutorialSetCanvas(GameObject canvas)
    {
        this.canvas = canvas;
    }


    //IEnumerator TutorialCloseRobotCanvas()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    cameraController.ZoomOut();
    //    TutorialManager.Instance.NextStep();
    //    //Destroy(this.gameObject);

    //}

    public void CheckIconDisplay()
    {

    }

    public void CreateRobotCanvas()
    {
        //Action　がProductionの時チェックボタン表示→クリックでキャンバス表示



        Camera.main.GetComponent<CameraController>().GetCenterIsland.GetComponent<IslandBase>().timer.GetComponent<Canvas>().enabled = false;


        createEnd = true;
        creating = false;
        Destroy(canvas);

        //robotCanvas = Instantiate(robotCanvasPrefab);
        //createRobot.SetActive(true);
        robotCanvas.transform.GetChild(2).GetComponent<ActionRobotInterface>().Create(createRobot);
        createRobot.transform.position = new Vector3(0, 0, 0);

        //checkMarkIcon.SetActive(false);
    }

    public void OnClickCheckMarkIcon()
    {
        cameraController.ZoomOut();
        checkMarkIcon.SetActive(false);
        //checkMarkIcon.GetComponent<MarkIcon>().SetWatched();

        if (TutorialManager.Instance.tutorialState != TutorialManager.TutorialState.No)
            TutorialCursor.Instance.SetActive(false);

        robotCanvas.SetActive(true);
        watched = true;
    }

    public void OnClickClossButton()
    {
        if(TutorialManager.Instance.tutorialState != TutorialManager.TutorialState.No)
        {
            TutorialManager.Instance.NextStep();
            //robotCanvas.SetActive(false);
            //Destroy(canvas);

            //return;
        }
        Destroy(canvas);

        robotCanvas.SetActive(false);
        watched = false;
        creating = false;
        createEnd = false;
        Name_Value.Instance.PlusProductionCount();

        audioSource.PlayOneShot(closeSound);

        //Name_Value.Instance.RankConfirm();
        //RankUpUI.Instance.RankUpCheck();
    }
}
