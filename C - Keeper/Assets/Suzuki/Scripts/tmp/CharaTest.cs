using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaTest : MonoBehaviour
{
    public GameObject charaObj;
    public GameObject instantiateObj;

    // Start is called before the first frame update
    void Start()
    {
        charaObj = (GameObject)Instantiate(instantiateObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
