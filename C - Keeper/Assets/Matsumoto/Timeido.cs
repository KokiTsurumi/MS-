using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeido : MonoBehaviour
{

    public Text text;

    // Use this for initialization
    void Start()
    {
        //最初のランク
        text.text = "ランク1";
    }

    // Update is called once per frame
    void Update()
    {
        //知名度があがりランクに変更があれば
        if (Input.GetMouseButtonDown(0))
        {
            text.text = "ランク2";
        }
    }

}