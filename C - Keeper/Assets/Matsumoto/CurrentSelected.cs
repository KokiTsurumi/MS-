using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurrentSelected : MonoBehaviour
{

    // eventSystemを取得するための変数宣言
    [SerializeField] EventSystem eventSystem;

    [SerializeField] private Image image;
   
    GameObject selectedObj;


    void Update()
    {

        // クリックされたタイミングで判定する
        if (Input.GetMouseButton(0))
        {
            // TryCatch文でNull対応
            try
            {
                // 子供のコンポーネントにアクセスしたいのでいったん変数に格納
                selectedObj = eventSystem.currentSelectedGameObject.gameObject;

                // ボタンの子供のTextコンポーネントからtextデータを取得
                image.sprite = selectedObj.GetComponentInChildren<Image>().sprite;
            }
            // 例外処理的なやつ
            catch (NullReferenceException ex)
            {
                // なにも選択されない場合に
                //image.sprite = "akiemon.png";
            }
        }
    }
}
