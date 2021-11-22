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

    [SerializeField]
    float zoomSpeed = 1.0f;
    [SerializeField]
    float zoomTime = 0.5f;
    [SerializeField]
    float cameraZoomSize = 2.5f;

    float cameraOrthSizeDefault;
    float zoomCurrentTime = 0;
    float cameraZ;


    [SerializeField]
    float transSpeed = 1.0f;
    [SerializeField]
    float transTime = 0.5f;

    float transCurrentTime = 0;
    Vector3 centerIslandPos;

    public GameObject targetIsland;

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

        zoomCurrentTime += Time.deltaTime * zoomSpeed;

        float easing = Easing.SineInOut(zoomCurrentTime, zoomTime, cameraOrthSizeDefault, cameraZoomSize);
        Camera.main.orthographicSize = easing;
        if(zoomCurrentTime >= zoomTime)
        {
            zoomCurrentTime = 0.0f;
            Camera.main.orthographicSize = cameraZoomSize;

            zoomState = ZoomState.STAY;

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

        transState = TransState.TRANSLATE_ISLAND;


        targetIsland = MouseManager.Instance.GetCursorOnObject();
        IslandManager.Instance.SetCurrentIsland(targetIsland);
        Vector3 tarPos = targetIsland.transform.position;
        Vector3 camPos = Camera.main.transform.position;

        startPos = new Vector3(camPos.x, camPos.y, cameraZ);
        endPos = new Vector3(tarPos.x, tarPos.y, cameraZ);
    }

    public void OnClickCenterIsland()
    {
        //transState = TransState.TRANSLATE_CENTER;
        if (!MouseManager.Instance.OnDoubleClickGameObject()) return;
        if (MouseManager.Instance.GetCurrentSelectedGameObject() != null) return;
        targetIsland = centerIsland;
        IslandManager.Instance.SetCurrentIsland(targetIsland);

        if(zoomState == ZoomState.DEFAULT)
            zoomState = ZoomState.IN;

        Vector3 tarPos = targetIsland.transform.position;
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