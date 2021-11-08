using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainCameraSC : MonoBehaviour
{
    [SerializeField]
    GameObject centerIsland;

    //lerpVector
    Vector3 startPositon;
    Vector3 endPositon;

    bool move = false;
    bool zoom = false;

    [SerializeField]
    float translateSpeed;

    [SerializeField]
    float zoomSpeed;

    //float moveCurrent = 0.0f;
    //float zoomCurrent = 0.0f;

    float currentTime = 0.0f;

    //float distance;

    float cameraZ;

    [SerializeField]    
    float cameraZoomSize = 2.5f;

    float cameraZDefault;

    enum CameraState
    {
        DEFAULT,
        ZOOM,
        TRANSLATE
    };

    CameraState cameraState = CameraState.DEFAULT;

    void Start()
    {
        cameraZ = transform.position.z;
        cameraZDefault = Camera.main.orthographicSize;
    }

    void Update()
    {
        if(MouseManagerSC.Instance.OnDoubleClick())
        {
            if(MouseManagerSC.Instance.GetCursorOnObject() == null)
            {
                LerpTranslationCenter();
            }
            else if(MouseManagerSC.Instance.GetCursorOnObject() == centerIsland)
            {
                LerpTransLationZoom();
            }
        }

        if(move)
        {
            currentTime += Time.deltaTime * translateSpeed;// /distance;

            transform.position = Vector3.Slerp(startPositon, endPositon, currentTime);


            if (Vector3.Distance(transform.position, endPositon) <= 0.0f)
            {
                move = false;
                currentTime = 0.0f;

                transform.position = endPositon;
            }

        }

        //if(zoom)
        //{
        //    ZoomIn();
        //}

        if (cameraState == CameraState.ZOOM)
            ZoomIn();
    }


    void ZoomIn()
    {
        

        currentTime += Time.deltaTime * zoomSpeed;

        float sinIn = EasingSC.SineInOut(currentTime, 0.5f, cameraZoomSize, cameraZDefault);
        //float sinIn = EasingSC.SineInOut(currentTime, 0.5f, cameraZDefault, cameraZoomSize);
        Camera.main.orthographicSize = sinIn;
        //Camera.main.orthographicSize = Mathf.Lerp(cameraZDefault, cameraZoomSize, currentTime);

        if (currentTime >= 0.5f)
        {
            zoom = false;
            currentTime = 0.0f;

            Camera.main.orthographicSize = cameraZoomSize;
            cameraState = CameraState.DEFAULT;
        }
    }

    //島をクリックしたとき、島オブジェクトが呼び出すEventTrigger
    public void LerpTranslation()
    {
        if (!MouseManagerSC.Instance.OnDoubleClick()) return;

        if (!move && !zoom)
            move = true;
        else
            return;

        Vector3 targetPosition = MouseManagerSC.Instance.GetCursorOnObject().transform.position;
        Vector3 cameraPosition = Camera.main.gameObject.transform.position;

        startPositon =  new Vector3(cameraPosition.x, cameraPosition.y,cameraZ);
        endPositon = new Vector3(targetPosition.x, targetPosition.y,cameraZ);
    }

    public void LerpTranslationCenter()
    {
        if (!move && !zoom)
            move = true;
        else
            return;


        Vector3 targetPosition = centerIsland.transform.position;
        Vector3 cameraPosition = Camera.main.gameObject.transform.position;

        startPositon = new Vector3(cameraPosition.x, cameraPosition.y, cameraZ);
        endPositon = new Vector3(targetPosition.x, targetPosition.y, cameraZ);

        //Camera.main.orthographicSize = cameraZoomDefault;
    }

    public void LerpTransLationZoom()
    {
        if (cameraState != CameraState.DEFAULT) return;

        cameraState = CameraState.ZOOM;
    }
}
