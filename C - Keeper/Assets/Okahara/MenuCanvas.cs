using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject canvas;

    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickMenuButton()
    {
        canvas.SetActive(true);
    }
}
