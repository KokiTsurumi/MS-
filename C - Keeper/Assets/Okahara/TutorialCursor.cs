using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCursor : SingletonMonoBehaviour<TutorialCursor>
{
    // Start is called before the first frame update

    [SerializeField] GameObject cursor; 


    public enum CursorPositionList
    {
        characterSelectSlotelft = 0,//キャラ選択の空きスロット左側
        characterSelectOkButton,//キャラ選択のオッケーボタン
        centerIsland,//拠点島
        investigationUI,//調査UI
        informationUI,//調査UI
        productionUI,//生産UI
        cleaningUI,//清掃UI
        robotSelectSlotleft,//清掃UI
        productionCenterIsland,//清掃UI
        menuButton,//メニュー
        menuExplanantion,//説明
        max
    }

    Vector2[] positionList = new Vector2[(int)CursorPositionList.max];

    void Start()
    {
        positionList[(int)CursorPositionList.characterSelectSlotelft] = new Vector2(-200, 70);
        positionList[(int)CursorPositionList.characterSelectOkButton] = new Vector2(35, -233);
        positionList[(int)CursorPositionList.centerIsland] = new Vector2(-150, -20);
        positionList[(int)CursorPositionList.investigationUI] = new Vector2(147, -33);
        positionList[(int)CursorPositionList.informationUI] = new Vector2(-456, -23);
        positionList[(int)CursorPositionList.productionUI] = new Vector2(-39, -191);
        positionList[(int)CursorPositionList.cleaningUI] = new Vector2(-300, -191);
        positionList[(int)CursorPositionList.robotSelectSlotleft] = new Vector2(-130,120);
        positionList[(int)CursorPositionList.productionCenterIsland] = new Vector2(-260,-55);
        positionList[(int)CursorPositionList.menuButton] = new Vector2(-380,-300);
        positionList[(int)CursorPositionList.menuExplanantion] = new Vector2(-130,80);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition(CursorPositionList list)
    {
        //Debug.Log("cursor position" + x + "," + y);
        cursor.GetComponent<RectTransform>().anchoredPosition = positionList[(int)list];

    }

    public void SetActive(bool set)
    {
        cursor.gameObject.SetActive(set);
    }

    public void SetScaleChange(int scale)
    {
        cursor.GetComponent<RectTransform>().localScale = new Vector3(scale,1,0);
    }
}
