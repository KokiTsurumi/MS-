using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMotion : MonoBehaviour
{
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float sin = Mathf.Sin(Time.time*4);
        transform.Translate(0.0f, sin/3,0.0f);
    }
}
