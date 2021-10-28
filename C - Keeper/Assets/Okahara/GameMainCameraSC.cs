using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainCameraSC : MonoBehaviour
{
    [SerializeField]
    GameObject centerIsland;

    Vector3 startPositon;
    Vector3 endPositon;

    bool move = false;
    bool zoom = false;

    [SerializeField]
    float translateSpeed;

    [SerializeField]
    float zoomSpeed;

    float moveCurrent = 0.0f;
    float zoomCurrent = 0.0f;

    //float distance;

    float cameraZ;

    float cameraZoomSize = 2.5f;
    float cameraZoomDefault;

    void Start()
    {
        cameraZ = transform.position.z;
        cameraZoomDefault = Camera.main.orthographicSize;
    }

    void Update()
    {
        if(CursorManagerSC.Instance.OnDoubleClick())
        {
            if(CursorManagerSC.Instance.GetCursorOnObject() == null)
            {
                LerpTranslationCenter();
            }
            else if(CursorManagerSC.Instance.GetCursorOnObject() == centerIsland)
            {
                LerpTransLationZoom();
            }
        }

        if (move)
        {
            moveCurrent += Time.deltaTime * translateSpeed;// / distance;

            transform.position = Vector3.Slerp(startPositon, endPositon, moveCurrent);


            if (Vector3.Distance(transform.position, endPositon) <= 0.0f)
            {
                move = false;
                moveCurrent = 0.0f;

                transform.position = endPositon;
            }

        }

        if(zoom)
        {
            zoomCurrent += Time.deltaTime * zoomSpeed;

            Camera.main.orthographicSize = Mathf.Lerp(cameraZoomDefault, cameraZoomSize, zoomCurrent);

            if(Camera.main.orthographicSize - cameraZoomSize <= 0.0f)
            {
                zoom = false;
                zoomCurrent = 0.0f;

                Camera.main.orthographicSize = cameraZoomSize;
            }
        }
    }

    //島をクリックしたとき、島オブジェクトが呼び出すEventTrigger
    public void LerpTranslation()
    {
        if (!CursorManagerSC.Instance.OnDoubleClick()) return;

        if (!move && !zoom)
            move = true;
        else
            return;

        Vector3 targetPosition = CursorManagerSC.Instance.GetCursorOnObject().transform.position;
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
        if (!zoom)
            zoom = true;
        else
            return;

        //Camera.main.orthographicSize = cameraZoomSize;
    }
}
