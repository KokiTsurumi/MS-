using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeMoveTexture : MonoBehaviour
{
    public enum TransMoveTexture
    {
        Wavemove,//波
        CloudMove,//雲
        shipMove
    }
    [SerializeField]
    float scrollSpeed;//背景をスクロールさせるスピード
    [SerializeField]
    float startLine;//背景のスクロールを開始する位置
    [SerializeField]
    float deadLine; //背景のスクロールが終了する位置
    [SerializeField]
    private TransMoveTexture Transmovetexture;
    [SerializeField]
    float anglex;
    [SerializeField]
    float angley;
    [SerializeField]
    float MoveWith;

    private Vector3 startPosition;//開始地点（戻る場所）

    void Start()
    {
        startPosition = transform.position;


    }
    void Update()
    {
        Scroll();
    }

    public void Scroll()
    {
        if (Transmovetexture == TransMoveTexture.CloudMove)
        {
            transform.Translate(-scrollSpeed, 0, 0); //x座標をscrollSpeed分動かす

            if (transform.position.x < deadLine) //もし背景のx座標よりdeadLineが大きくなったら
            {
                transform.position = startPosition;//背景をstartLineまで戻す
            }
        }
        if (Transmovetexture == TransMoveTexture.Wavemove)
        {
            anglex += Time.deltaTime * 2;
            angley += Time.deltaTime * 2;
            transform.Translate(Mathf.Sin(anglex) * MoveWith,
                Mathf.Sin(angley) * MoveWith, 0); //x座標をscrollSpeed分動かす

            if (transform.position.y < deadLine) //もし背景のx座標よりdeadLineが大きくなったら
            {
                transform.position = startPosition;//背景をstartLineまで戻す
            }
        }
        if (Transmovetexture == TransMoveTexture.shipMove)
        {
            angley += Time.deltaTime * 2;
            transform.Translate(0,Mathf.Sin(angley) * MoveWith, 0); //x座標をscrollSpeed分動かす

            if (transform.position.y < deadLine) //もし背景のx座標よりdeadLineが大きくなったら
            {
                transform.position = startPosition;//背景をstartLineまで戻す
            }
        }
    }
}

