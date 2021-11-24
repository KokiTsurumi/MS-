using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    private float Gage = 1000.0f;
    private float _Gage = 0.1f;
    private Image _image;
    [SerializeField] float DeadCount;
    private int Count;
    // Start is called before the first frame update
    void Start()
    {
        _image = this.GetComponent<Image>();//æ“¾•Ši”[
    }

    // Update is called once per frame
    void Update()
    {
        Gage -= _Gage;

        Count++;//0.7~0.4
        if (Count >= DeadCount )
        {
            Count = 0;
            _Gage *= -1;
        }
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    Gage--;
        //}
        //else if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    Gage++;
        //}
        //Å‘å’l‚ğŠ„‚èZ‚µ‚Ä”ä—¦“Ë‚Á‚Ş
        _image.fillAmount = Gage / 1000.0f;
    }
}
