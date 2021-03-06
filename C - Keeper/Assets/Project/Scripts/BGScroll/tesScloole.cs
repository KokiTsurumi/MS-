using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//背景のサイズが横920縦242.5の時縦軸だけ調整したもの今後好きにいじれるよう調整します。
public class tesScloole : MonoBehaviour
{
    [SerializeField] float scrollSpeed; //背景をスクロールさせるスピード
    [SerializeField] float startLine;//背景のスクロールを開始する位置
    [SerializeField] float deadLine; //背景のスクロールが終了する位置
    [SerializeField] float CanvasY;//画像の高さ
    //private Vector3 startPosition;//開始地点（戻る場所）

    void Start()
    {
        //startPosition = transform.position;//入力なければそのままポジション入れちゃう
    }
    void Update()
    {
        Scroll();
    }

    public void Scroll()
    {
        transform.Translate(scrollSpeed, 0, 0); //x座標をscrollSpeed分動かす

        if (transform.position.x > deadLine) //もし背景のx座標よりdeadLineが大きくなったら
        {
            //transform.position = startPosition;//背景をstartLineまで戻す
            transform.position = new Vector3(startLine, CanvasY, 0);//背景をstartLineまで戻す

        }

    }
}

