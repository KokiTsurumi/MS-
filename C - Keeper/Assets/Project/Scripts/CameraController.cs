using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラコントロール　クラス
/// <para>・ZoomIn、Out、カメラ移動、アクションUI管理　等</para>
/// </summary>
public class CameraController : MonoBehaviour
{
    Animator actionCanvasAnimator;

    [SerializeField] GameObject centerIsland;
    public GameObject GetCenterIsland => centerIsland;

    [SerializeField] public GameObject backButton;

    float zoomSpeed = 1.0f;
    float zoomTime = 0.5f;
    float cameraZoomSize = 2.5f;

    float cameraOrthSizeDefault;
    float zoomCurrentTime = 0;
    float cameraZ;


    float transSpeed = 1.0f;
    float transTime = 0.5f;

    float transCurrentTime = 0;
    Vector3 centerIslandPos;


    Vector3 startPos;
    Vector3 endPos;

    
    enum ZoomState
    {
        DEFAULT,
        IN,
        OUT,
        STAY
    }

    enum TransState
    {
        CENTER,
        ISLAND,          
        TRANSLATE_ISLAND,   //各島移動中
        TRANSLATE_CENTER,   //拠点島移動中
        CHOICE,
    }

    [SerializeField] ZoomState zoomState = ZoomState.DEFAULT;
    [SerializeField] TransState transState = TransState.CENTER;


    [SerializeField]  bool action = false;
    public bool GetActionBool => action;

    [SerializeField] GameObject actionGameObject;

    [SerializeField] Canvas gameMainCanvas;

    void Start()
    {

        cameraZ = transform.position.z;
        cameraOrthSizeDefault = Camera.main.orthographicSize;
        
        centerIsland = GameObject.Find("Island_center").gameObject;
        centerIslandPos = centerIsland.transform.position;
        centerIslandPos.z = cameraZ;//カメラのZ座標に直す

        actionCanvasAnimator = GameObject.Find("ActionCanvas").GetComponent<Animator>();
        cameraZ = transform.position.z;
        cameraOrthSizeDefault = Camera.main.orthographicSize;

        backButton.SetActive(false);
    }

    void Update()
    {
        UpdateZoomIn();
        UpdateZoomOut();
        UpdateTranslation();
        UpdateTranslationCenter();


        //カメラが俯瞰の時、Timer、Icon、海開放アニメーションを突っ込む
        //タイマー非表示
        if (zoomState == ZoomState.IN)
        {
            //GameObject island = IslandManager.Instance.GetCurrentIsland();
            centerIsland.GetComponent<IslandBase>().timer.GetComponent<Canvas>().enabled = false;

            foreach (GameObject obj in IslandManager.Instance.islandList)
            {
                obj.GetComponent<IslandBase>().timer.GetComponent<Canvas>().enabled = false;
                obj.GetComponent<IslandBase>().icon.GetComponent<Canvas>().enabled = false;
            }
        }
        else if(zoomState == ZoomState.DEFAULT)
        {
            centerIsland.GetComponent<IslandBase>().timer.GetComponent<Canvas>().enabled = true;

            //海清掃アニメーション
            foreach (GameObject obj in IslandManager.Instance.islandList)
            {
                if(obj.GetComponent<IslandBase>().GetPollutionLevel() <= 0)
                {
                    if(obj.transform.GetChild(0).GetComponent<SeaDizolve>()!= null)
                        obj.transform.GetChild(0).GetComponent<SeaDizolve>().start = true;


                }

                obj.GetComponent<IslandBase>().timer.GetComponent<Canvas>().enabled = true;

                if(obj.GetComponent<IslandBase>().icon.GetComponent<MarkIcon>().GetWatched != true)
                    obj.GetComponent<IslandBase>().icon.GetComponent<Canvas>().enabled = true;
            }
        }

    }

    void UpdateZoomIn()
    {
        if (zoomState != ZoomState.IN) return;
        if (transState == TransState.CHOICE)
        {
            transState = TransState.CENTER;
            action = false;
        }

        gameMainCanvas.enabled = false;

        zoomCurrentTime += Time.deltaTime * zoomSpeed;

        float easing = Easing.SineInOut(zoomCurrentTime, zoomTime, cameraOrthSizeDefault, cameraZoomSize);
        Camera.main.orthographicSize = easing;
        if(zoomCurrentTime >= zoomTime)
        {
            zoomCurrentTime = 0.0f;
            Camera.main.orthographicSize = cameraZoomSize;

            zoomState = ZoomState.STAY;

            //ポップアップ表示非表示
            if (transState == TransState.CENTER)
                actionCanvasAnimator.SetBool("popOut", true);
            else if (transState == TransState.ISLAND)
                DisplayUI();


        }

    }

    void UpdateZoomOut()
    {
        if (zoomState != ZoomState.OUT) return;

        if (actionCanvasAnimator.GetBool("popOut"))
            actionCanvasAnimator.SetBool("popOut", false);

        zoomCurrentTime += Time.deltaTime * zoomSpeed;

        float easing = Easing.SineInOut(zoomCurrentTime, zoomTime, cameraZoomSize, cameraOrthSizeDefault);
        Camera.main.orthographicSize = easing;

        if(zoomCurrentTime >= zoomTime)
        {
            zoomCurrentTime = 0.0f;

            Camera.main.orthographicSize = cameraOrthSizeDefault;
            zoomState = ZoomState.DEFAULT;

            if (transState == TransState.ISLAND)
                transState = TransState.TRANSLATE_CENTER;

            gameMainCanvas.enabled = true;

        }
    }

    void UpdateTranslation()
    {
        if (transState != TransState.TRANSLATE_ISLAND) return;


        transCurrentTime += Time.deltaTime * transSpeed;

        float x = Easing.ExpOut(transCurrentTime, transTime, startPos.x, endPos.x);
        float y = Easing.ExpOut(transCurrentTime, transTime, startPos.y, endPos.y);
        float z = cameraZ;
        Camera.main.transform.position = new Vector3(x, y, z);

        if(transCurrentTime >= transTime)
        {
            transCurrentTime = 0.0f;
            Camera.main.transform.position = endPos;
            transState = TransState.ISLAND;
            zoomState = ZoomState.IN;

            
        }

    }

    void UpdateTranslationCenter()
    {
        //作業完了後

        if (transState != TransState.TRANSLATE_CENTER) return;


        transCurrentTime += Time.deltaTime * transSpeed;

        float x = Easing.ExpOut(transCurrentTime, transTime, startPos.x, centerIslandPos.x);
        float y = Easing.ExpOut(transCurrentTime, transTime, startPos.y, centerIslandPos.y);
        float z = cameraZ;
        Camera.main.transform.position = new Vector3(x, y, z);

        if (transCurrentTime >= transTime)
        {
            transCurrentTime = 0.0f;
            Camera.main.transform.position = centerIslandPos;
            transState = TransState.CENTER;

        }
    }

    public void OnClickIsland()
    {
        if (transState != TransState.CHOICE) return;

        GameObject island = MouseManager.Instance.GetCursorOnObject();
        IslandBase.STATE_ISLAND state = island.GetComponent<IslandBase>().state;
        
        
        //作業の矛盾を防ぐ条件文
        //未調査
        if(state == IslandBase.STATE_ISLAND.STATE_UNINVESTIGATED)
        {
            if (actionGameObject.name == "Cleaning")
            {
                Debug.Log(island.name + "まだ調査が完了していません");
                return;
            }
            else if (actionGameObject.name == "Information")
            {
                Debug.Log(island.name + "は調査をしていないため、情報がありません");
                return;
            }
        }
        //調査中
        else if (state == IslandBase.STATE_ISLAND.STATE_INVESTIGATING)
        {
            if(actionGameObject.name == "Investigation")
            {
                Debug.Log(island.name + "は調査中です");
                return;
            }
            else if(actionGameObject.name == "Cleaning")
            {
                Debug.Log(island.name + "は調査中です");
                return;
            }
            else if(actionGameObject.name == "Information")
            {
                Debug.Log(island.name + "は調査をしていないため、情報がありません");
                return;
            }
        }
        //調査済
        else if (state == IslandBase.STATE_ISLAND.STATE_INVESTIGATED)
        {
            if (actionGameObject.name == "Investigation")
            {
                Debug.Log(island.name + "は調査済です");
                return;
            }
        }
        //清掃
        else if(state == IslandBase.STATE_ISLAND.STATE_CLEANING)
        {
            if(actionGameObject.name == "Investigation")
            {
                Debug.Log(island.name + "は調査済です");
                return;
            }
            else if(actionGameObject.name == "Cleaning" 
                && island.GetComponent<IslandBase>().timer.GetComponent<Timer>().GetCurrentTime() >= 0)
            {
                Debug.Log(island.name + "は清掃中です");
                return;
            }
        }
        //清掃完了
        else if(state == IslandBase.STATE_ISLAND.STATE_CLEANED)
        {
            if(actionGameObject.name == "Investigation")
            {
                Debug.Log("清掃が完了しました");
                return;
            }
            else if(actionGameObject.name == "Cleaning")
            {
                Debug.Log("清掃が完了しました");
                return;
            }
        }



            
        transState = TransState.TRANSLATE_ISLAND;

        IslandManager.Instance.SetCurrentIsland(island);
        Vector3 tarPos = IslandManager.Instance.GetCurrentIsland().transform.position;
        Vector3 camPos = Camera.main.transform.position;

        startPos = new Vector3(camPos.x, camPos.y, cameraZ);
        endPos = new Vector3(tarPos.x, tarPos.y, cameraZ);
    }

    public void OnClickCenterIsland()
    {
        if (!MouseManager.Instance.OnDoubleClickGameObject()) return;
        if (MouseManager.Instance.GetCurrentSelectedGameObject() != null) return;
        IslandManager.Instance.SetCurrentIsland(MouseManager.Instance.GetCursorOnObject());

        if(zoomState == ZoomState.DEFAULT)
            zoomState = ZoomState.IN;


        Vector3 tarPos = IslandManager.Instance.GetCurrentIsland().transform.position;
        Vector3 camPos = Camera.main.transform.position;

        startPos = new Vector3(camPos.x, camPos.y, cameraZ);
        endPos = new Vector3(tarPos.x, tarPos.y, cameraZ);
    }

  
    public void OnClickBackGround()
    {
        if (!MouseManager.Instance.OnDoubleClickGameObject()) return;
        if (MouseManager.Instance.GetCurrentSelectedGameObject() != null) return;
        if (action) return;
        if (TutorialManager.Instance.tutorialState != TutorialManager.TutorialState.No) return;

        if (zoomState == ZoomState.STAY)
            zoomState = ZoomState.OUT;

        if (transState == TransState.ISLAND)
            transState = TransState.TRANSLATE_CENTER;
    }

    public void ZoomOut()
    {
        if(zoomState != ZoomState.DEFAULT)
           zoomState = ZoomState.OUT;
        Debug.Log("画面ズームアウト");
    }

    public void ZoomIn()
    {
        zoomState = ZoomState.IN;
    }


    //作業が完了して拠点島に移動
    public void TranslateCenterIsland()
    {
        action = false;
        zoomState = ZoomState.OUT;

        Vector3 tarPos = centerIslandPos;
        Vector3 camPos = Camera.main.transform.position;

        startPos = new Vector3(camPos.x, camPos.y, cameraZ);
        endPos = new Vector3(tarPos.x, tarPos.y, cameraZ);

    }

    public void ActionStart()
    {

        TutorialManager.TutorialState tutorialState = TutorialManager.Instance.tutorialState;
        if (tutorialState != TutorialManager.TutorialState.No)
        {

            IslandManager.Instance.SetCurrentIsland(centerIsland);

            TutorialStep("Investigation", TutorialManager.TutorialState.Investigation);
            TutorialStep("Production", TutorialManager.TutorialState.Production);
            TutorialStep("Cleaning", TutorialManager.TutorialState.Cleanning);
            TutorialStep("Information", TutorialManager.TutorialState.Information);

            return;
        }

        action = true;
        actionCanvasAnimator.SetBool("popOut", false);

        actionGameObject = MouseManager.Instance.GetCurrentSelectedGameObject();
        actionGameObject.GetComponent<ActionButtonInterface>().ActionStart();

    }

    void TutorialStep(string actionButtonName,TutorialManager.TutorialState state)
    {
        if (MouseManager.Instance.GetCurrentSelectedGameObject().name != actionButtonName)
            return;

        TutorialManager.TutorialState tutorialState = TutorialManager.Instance.tutorialState;

        if (tutorialState != state)
            return;


        action = true;
        actionCanvasAnimator.SetBool("popOut", false);
        TutorialManager.Instance.NextStep();
    }

    public void ActionEnd()
    {
        action = false;
        actionGameObject.GetComponent<ActionButtonInterface>().ActionEnd();
    }

    
    public void DisplayUI()
    {
        actionGameObject.GetComponent<ActionButtonInterface>().DisplayUI();
    }

    public void IslandChoice()
    {
        transState = TransState.CHOICE;

    }

    public ActionButtonInterface GetCurrntAction()
    {
        return actionGameObject.GetComponent<ActionButtonInterface>();
    }

    public void ActionButtonRepop()
    {
        action = false;
        actionCanvasAnimator.SetBool("popOut", true);
    }
}