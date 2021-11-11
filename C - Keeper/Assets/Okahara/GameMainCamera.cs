using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainCamera : MonoBehaviour
{
    [SerializeField]
    GameObject centerIsland;

 
    [SerializeField]
    Animator actionCanvasAnimator;

    //[SerializeField]
    //GameObject investigationCanvas;

    [Header("カメラ移動パラメータ――――――――――――")]


    [SerializeField]
    float translateSpeed = 1.0f;
    [SerializeField]
    float transTime = 0.5f;

    [Header("カメラズームパラメータ――――――――――――")]

    [SerializeField]
    float zoomSpeed = 1.0f;

    [SerializeField]    
    float cameraZoomSize = 2.5f;

    [SerializeField]
    float zoomTime = 0.5f;

    
    enum ZoomState
    {
        DEFAULT,
        ZOOM_IN,
        ZOOM_OUT,
        STAY
    };

    enum TransState
    {
        DEFAULT,
        TRANSLATE,
        TRANSLATE_CENTER
    }

    [SerializeField]
    ZoomState zoomState = ZoomState.DEFAULT;
    [SerializeField]
    TransState transState = TransState.DEFAULT;

    GameObject target;

    Vector3 startPositon;
    Vector3 endPositon;

    float cameraOrthSizeDefault;

    float zoomCurrentTime = 0.0f;
    float transCurrentTime = 0.0f;

    float cameraZ;

    GameObject buttonCurrent;

    void Start()
    {
        cameraZ = transform.position.z;
        cameraOrthSizeDefault = Camera.main.orthographicSize;
    }

    void Update()
    {
        GameObject cursorObj = MouseManagerSC.Instance.GetCursorOnObject();

        MouseManagerSC.Instance.GetCursorOnButton();
        if (MouseManagerSC.Instance.OnDoubleClick() && MouseManagerSC.Instance.GetCursorOnButton() == null)
        {
         
            if(cursorObj.tag == "BackGround" && zoomState == ZoomState.STAY)
            {
                zoomState = ZoomState.ZOOM_OUT;
                Debug.Log("ZoomOut");
                if(Camera.main.transform.position.x != centerIsland.transform.position.x)
                {
                    TranslateCenter();
                }
            }
            else if(cursorObj.tag == "Center Island" && zoomState != ZoomState.STAY)
            {
                zoomState = ZoomState.ZOOM_IN;
            }
        }

    

        ZoomIn();
        ZoomOut();
        Translation();
        TranslationCenter();
    }


    public void ZoomIn()
    {

        if (zoomState != ZoomState.ZOOM_IN) return;

        

        zoomCurrentTime += Time.deltaTime * zoomSpeed;

        float easing = EasingSC.SineInOut(zoomCurrentTime, zoomTime, cameraOrthSizeDefault, cameraZoomSize);
        
        Camera.main.orthographicSize = easing;

        if (zoomCurrentTime >= zoomTime)
        {
            zoomCurrentTime = 0.0f;

            Camera.main.orthographicSize = cameraZoomSize;
            zoomState = ZoomState.STAY;

            if (Camera.main.transform.position.x == centerIsland.transform.position.x && !actionCanvasAnimator.GetBool("popOut"))
                actionCanvasAnimator.SetBool("popOut", true);
            else
                buttonCurrent.GetComponent<ActionButtonInterface>().DisplayUI();


        }
    }

    public void ZoomOut()
    {
        if (zoomState != ZoomState.ZOOM_OUT) return;

        if (actionCanvasAnimator.GetBool("popOut"))
            actionCanvasAnimator.SetBool("popOut",false);

        zoomCurrentTime += Time.deltaTime * zoomSpeed;

        float easing = EasingSC.SineInOut(zoomCurrentTime, zoomTime, cameraZoomSize, cameraOrthSizeDefault);

        Camera.main.orthographicSize = easing;

        if (zoomCurrentTime >= 0.5f)
        {
            zoomCurrentTime = 0.0f;

            Camera.main.orthographicSize = cameraOrthSizeDefault;
            zoomState = ZoomState.DEFAULT;
        }
    }

  
    void Translation()
    {
        if (transState != TransState.TRANSLATE) return;

        transCurrentTime += Time.deltaTime * translateSpeed;

        Vector3 pos = transform.position;
        pos.x = EasingSC.ExpOut(transCurrentTime, transTime, startPositon.x, endPositon.x);
        pos.y = EasingSC.ExpOut(transCurrentTime, transTime, startPositon.y, endPositon.y);

        Camera.main.transform.position = pos;
        
        if(transCurrentTime >= transTime)
        {
            transCurrentTime = 0.0f;
            Camera.main.transform.position = endPositon;
            transState = TransState.DEFAULT;

            if(endPositon != centerIsland.transform.position)
            {
                zoomState = ZoomState.ZOOM_IN;
            }
        }

     
    }

    void TranslationCenter()
    {

        if (transState != TransState.TRANSLATE_CENTER) return;

        transCurrentTime += Time.deltaTime * translateSpeed;

        Vector3 pos = transform.position;
        pos.x = EasingSC.ExpOut(transCurrentTime, transTime, startPositon.x, centerIsland.transform.position.x);
        pos.y = EasingSC.ExpOut(transCurrentTime, transTime, startPositon.y, centerIsland.transform.position.y);

        Camera.main.transform.position = pos;

        if (transCurrentTime >= transTime)
        {
            transCurrentTime = 0.0f;
            //Camera.main.transform.position = centerIsland.transform.position;
            transState = TransState.DEFAULT;
        }
    }

    public void ActionClick()
    {
        if(zoomState == ZoomState.STAY)
            zoomState = ZoomState.ZOOM_OUT;

        buttonCurrent = MouseManagerSC.Instance.GetCurrentSelectedGameObject();
        //actionCanvas.SetActive(false);
    }



    public void TranslationIsland()
    {

        transState = TransState.TRANSLATE;
        target = MouseManagerSC.Instance.GetCursorOnObject();

        Vector3 targetPosition = target.transform.position;
        Vector3 cameraPosition = Camera.main.gameObject.transform.position;
       
        startPositon = new Vector3(cameraPosition.x, cameraPosition.y, cameraZ);
        endPositon = new Vector3(targetPosition.x, targetPosition.y, cameraZ);
    }

    void TranslateCenter()
    {
        transState = TransState.TRANSLATE_CENTER;

        Vector3 targetPosition = centerIsland.transform.position;
        Vector3 cameraPosition = Camera.main.gameObject.transform.position;

        startPositon = new Vector3(cameraPosition.x, cameraPosition.y, cameraZ);
        endPositon = new Vector3(targetPosition.x, targetPosition.y, cameraZ);
    }
}
