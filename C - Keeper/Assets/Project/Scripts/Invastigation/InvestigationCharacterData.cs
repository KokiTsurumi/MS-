using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestigationCharacterData : ActionCharacterData
{
    public InvestigationCanvas canvas;
   
    override public void onClick()
    {
        canvas.CharaDataBack();
    }

    override public void Create()
    {
        GameObject root = transform.root.gameObject;
        canvas = root.transform.GetChild(0).GetComponent<InvestigationCanvas>();

    }

}
