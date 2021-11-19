using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseManager : SingletonMonoBehaviour<MouseManager>
{
    [SerializeField] GameObject cursorOnGameObject;
    [SerializeField] GameObject currentSelectedGameObject;

    //MouseInput•ûŽ®
    bool isDoubleClickStart = false;
    float doubleClickTime;
    bool doubleClick = false;

    //Raycast•ûŽ®
    [SerializeField, Header("Raycast•ûŽ®")] GameObject raycast;
    [SerializeField] bool isDoubleClickGameObjectStart = false;
    [SerializeField] bool doubleClickGameObject = false;
    float doubleClickGameObjectTime;

    //Eventsystem•ûŽ®
    [SerializeField,Header("Eventsystem•ûŽ®")] GameObject selectUI;
    [SerializeField] bool isDoubleClickUIStart = false;
    [SerializeField] bool doubleClickUI = false;
    float doubleClickUITime;

    [SerializeField]
    GameObject test;

    void Update()
    {
        if(isDoubleClickStart)
        {
            doubleClickTime += Time.deltaTime;
            if(doubleClickTime < 0.3f)
            {
                if (Input.GetMouseButtonDown(0)){

                    isDoubleClickStart = false;
                    doubleClick = true;
                    doubleClickTime = 0.0f;
                }
            }
            else
            {
                doubleClick = false;

                isDoubleClickStart = false;
                doubleClickTime = 0.0f;
            }

        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                isDoubleClickStart = true;
            }
        }

        if (isDoubleClickUIStart)
        {
            doubleClickUITime += Time.deltaTime;
            if (doubleClickUITime < 0.3f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (selectUI != EventSystem.current.currentSelectedGameObject)
                        return;

                    isDoubleClickUIStart = false;
                    doubleClickUI = true;
                    doubleClickUITime = 0.0f;
                }
            }
            else
            {
                doubleClickUI = false;

                isDoubleClickUIStart = false;
                doubleClickUITime = 0.0f;
            }

        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                isDoubleClickUIStart = true;
                selectUI = EventSystem.current.currentSelectedGameObject;

            }
        }

        if (isDoubleClickGameObjectStart)
        {
            doubleClickGameObjectTime += Time.deltaTime;
            if (doubleClickGameObjectTime < 0.3f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    
                    if (raycast != hit)
                        return;

                    isDoubleClickGameObjectStart = false;
                    doubleClickGameObject = true;
                    doubleClickGameObjectTime = 0.0f;
                }
            }
            else
            {
                doubleClickGameObject = false;

                isDoubleClickGameObjectStart = false;
                doubleClickGameObjectTime = 0.0f;
            }

        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    raycast = hit.collider.gameObject;
                    isDoubleClickGameObjectStart = true;

                }

            }
        }

        GetCursorOnObject();
    }

    public GameObject GetCursorOnObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            cursorOnGameObject = hit.collider.gameObject;
        }
        else
        {
            cursorOnGameObject = null;
        }

        return cursorOnGameObject;
    }


    public bool OnDoubleClick()
    {
        return doubleClick;
    }

    public bool OnDoubleClickUI()
    {
        return doubleClickUI;
    }

    public bool OnDoubleClickGameObject()
    {
        return doubleClickGameObject;
    }

    public GameObject GetCurrentSelectedGameObject()
    {
        currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
        return currentSelectedGameObject;
    }
}
