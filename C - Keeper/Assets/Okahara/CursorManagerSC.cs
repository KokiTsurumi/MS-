using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagerSC : SingletonMonoBehaviour<CursorManagerSC>
{
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
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    public bool OnDoubleClick()
    {
        return doubleClick;
    }
}
