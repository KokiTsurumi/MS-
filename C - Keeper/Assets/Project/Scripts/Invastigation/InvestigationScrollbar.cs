using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestigationScrollbar : MonoBehaviour
{
    [SerializeField]
    float value = 1.0f;

    bool set = false;

    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(set)
        {
            this.GetComponent<Scrollbar>().value = value;
            set = false;
        }
    }

    public void ScrollbarPositionReset()
    {
        set = true;
    }
}
