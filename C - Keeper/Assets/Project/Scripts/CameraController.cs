using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Animator actionCanvasAnimator;

    [SerializeField]
    GameObject centerIsland;

    [SerializeField]
    public GameObject backButton;

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

    [SerializeField]
    ZoomState zoomState = ZoomState.DEFAULT;

    [SerializeField]
    TransState transState = TransState.CENTER;

 

    [SerializeField]
    bool action = false;

    [SerializeField]
    GameObject actionGameObject;

    [SerializeField]
    Canvas gameMaimCanvas;

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
        if (transState == TransState.CENTER && zoomState == ZoomState.DEFAULT)
        {
            GameObject island = IslandManager.Instance.GetCurrentIsland();

            //if(island.GetComponent<IslandBase>().state == IslandBase.STATE_ISLAND.STATE_INVESTIGATED)

            //タイマー表示
            //for (int i = 0; i < IslandManager.Instance.islandList.Count; i++)
            //{
            //    GameObject island = IslandManager.Instance.islandList[i];
            //    island.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
            //}
        }
        else
        {
            for (int i = 0; i < IslandManager.Instance.islandList.Count; i++)
            {
                GameObject island = IslandManager.Instance.islandList[i];
                island.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
            }
        }

    }

    void UpdateZoomIn()
    {
        //拠点島をクリックしたとき
        //各島を選択して、移動した後

        if (zoomState != ZoomState.IN) return;
        if (transState == TransState.CHOICE)
        {
            transState = TransState.CENTER;
            action = false;
        }

        gameMaimCanvas.enabled = false;

        zoomCurrentTime += Time.deltaTime * zoomSpeed;

        float easing = Easing.SineInOut(zoomCurrentTime, zoomTime, cameraOrthSizeDefault, cameraZoomSize);
        Camera.main.orthographicSize = easing;
        if(zoomCurrentTime >= zoomTime)
        {
            zoomCurrentTime = 0.0f;
            Camera.main.orthographicSize = cameraZoomSize;

            zoomState = ZoomState.STAY;

            ////タイマー非表示
            //if(transState == TransState.CENTER)
            //{
            //    for (int i = 0; i < IslandManager.Instance.islandList.Count; i++)
            //    {
            //        GameObject island = IslandManager.Instance.islandList[i];
            //        island.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
            //    }
            //}

            //ポップアップ表示非表示
            if (transState == TransState.CENTER)
                actionCanvasAnimator.SetBool("popOut", true);
            else if (transState == TransState.ISLAND)
                DisplayUI();


        }

    }

    void UpdateZoomOut()
    {
        //BackGroundを押したとき
        //各島で作業完了後、完了UI表示後

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

            ////タイマー表示
            //for (int i = 0; i < IslandManager.Instance.islandList.Count; i++)
            //{
            //    GameObject island = IslandManager.Instance.islandList[i];
            //    island.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
            //}

            gameMaimCanvas.enabled = true;

        }
    }

    void UpdateTranslation()
    {
        //各島押されたとき
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

        //調査を押したときに調査済なら反応させない
        GameObject island = MouseManager.Instance.GetCursorOnObject();
        if (island.GetComponent<IslandBase>().state != IslandBase.STATE_ISLAND.STATE_UNINVESTIGATED
            &&
            actionGameObject.name == "Investigation")
        {
            Debug.Log(island.name + "は調査済です");
            return;
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
        //transState = TransState.TRANSLATE_CENTER;
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

        if (zoomState == ZoomState.STAY)
            zoomState = ZoomState.OUT;

        if (transState == TransState.ISLAND)
            transState = TransState.TRANSLATE_CENTER;
    }

    public void ZoomOut()
    {
        zoomState = ZoomState.OUT;
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

            if (MouseManager.Instance.GetCurrentSelectedGameObject().name == "Investigation")
            {
                if (tutorialState == TutorialManager.TutorialState.Investigation)
                {
                    TutorialManager.Instance.NextStep();
                    action = true;
                    actionCanvasAnimator.SetBool("popOut", false);
                    return;
                }

            }
            else if (MouseManager.Instance.GetCurrentSelectedGameObject().name == "Production")
            {
                if (tutorialState == TutorialManager.TutorialState.Production)
                {
                    TutorialManager.Instance.NextStep();
                    action = true;
                    actionCanvasAnimator.SetBool("popOut", false);
                    return;
                }

            }

            return;
        }

        action = true;
        actionCanvasAnimator.SetBool("popOut", false);

        actionGameObject = MouseManager.Instance.GetCurrentSelectedGameObject();
        actionGameObject.GetComponent<ActionButtonInterface>().ActionStart();

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