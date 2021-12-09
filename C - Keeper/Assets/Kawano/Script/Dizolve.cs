using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dizolve : MonoBehaviour
{
    Material DizolveMaterial;

    private void Start()
    {
        DizolveMaterial = GetComponent<Renderer>().material;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            DizolveMaterial.SetFloat("Threshold", 0.5f);
        }
       
    }
}
