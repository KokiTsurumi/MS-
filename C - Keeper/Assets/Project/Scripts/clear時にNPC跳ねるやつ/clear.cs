using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class clear : MonoBehaviour
{
    public RectTransform npcpos;
    public int MaxCount=500;
    public float spead = 0.05f;
    private int count=0;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        npcpos.position += new Vector3(0, spead, 0);
        count++;
        if (count == MaxCount)
        {
            count = 0;
            spead *= -1;
            
        }
    }
}
