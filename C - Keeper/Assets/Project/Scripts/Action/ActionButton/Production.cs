using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Production : ActionButtonInterface
{
    [SerializeField]
    GameObject productionUI;


    GameObject canvas;

    
    override public void ActionStart()
    {
        cameraController.backButton.SetActive(true);

        canvas = (GameObject)Instantiate(productionUI);
        canvas.transform.GetChild(0).GetComponent<ProductionCanvas>().Initialize();
    }

    public override void ActionEnd()
    {

        cameraController.backButton.SetActive(false);



        Destroy(canvas);

        cameraController.ActionButtonRepop();
    }

}
