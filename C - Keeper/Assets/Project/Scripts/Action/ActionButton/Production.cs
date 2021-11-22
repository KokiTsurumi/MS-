﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Production : ActionButtonInterface
{
    [SerializeField]
    GameObject productionUI;

    //bool doing = false;

    GameObject canvas;

    
    override public void ActionStart()
    {
        canvas = (GameObject)Instantiate(productionUI);
        canvas.transform.GetChild(0).GetComponent<ProductionCanvas>().Initialize();
    }

    public override void ActionEnd()
    {


        


        Destroy(canvas);

        cameraController.ActionButtonRepop();
    }

}