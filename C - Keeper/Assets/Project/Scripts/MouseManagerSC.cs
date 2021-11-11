using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseManagerSC : SingletonMonoBehaviour<MouseManagerSC>
{
    [SerializeField]
    GameObject cursorGameObject;
    [SerializeField]
    GameObject cursorUI;

    bool isDoubleClickStart = false;
    float doubleClickTime;
    bool doubleClick = false;

   


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
    }

    public GameObject GetCursorOnObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if(hit.collider != null)
        {
            cursorGameObject = hit.collider.gameObject;
            return hit.collider.gameObject;
        }
        else
        {
            cursorGameObject = null;
            return null;
        }
    }

    public GameObject GetCursorOnButton()
    {
        //EventSystem eventSystem = EventSystem.current;
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        
        if (obj == null)
        {
            return null;

        }



        return obj;
        //if (obj != null)
        //    cursorUI = obj;

        //return obj;
        //return cursorUI;
    }

    public void GetButtonList()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current); ;
        EventSystem.current.RaycastAll(pointerEventData, results);
        foreach(RaycastResult target in results)
        {
            Debug.Log(target.gameObject.name);
        }
    }


    //public void SetCursorUI(GameObject obj)
    //{
    //    cursorUI = obj;
    //}

    public bool OnDoubleClick()
    {
        return doubleClick;
    }

    public GameObject GetCurrentSelectedGameObject()
    {
        return EventSystem.current.currentSelectedGameObject;
    }
}
