using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kariMoveobutu : MonoBehaviour
{

    [SerializeField] int counter;
    [SerializeField] float scrollSpeed; //スクロールさせるスピード
    [SerializeField] int deadcounter; //カウント終了ライン

    private Vector3 startPosition;//開始地点（戻る場所）
    void Start()
    {
        startPosition = transform.position;
       
    }
    void Update()
    {
        Move();
    }
    public void Move()
    {
        transform.Translate(scrollSpeed, 0, 0);
        counter++;
        if (counter == deadcounter)
        {
            counter = 0;
            scrollSpeed *= -1;
        }
    }
}
