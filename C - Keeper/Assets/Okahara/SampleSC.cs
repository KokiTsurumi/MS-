using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSC : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject obj;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            obj.SetActive(true);
        }
    }
}
