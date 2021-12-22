using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandCursor : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject islandImage;

    CameraController cameraController;

    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraController.GetTransState != CameraController.TransState.CHOICE)
            return;



        if (MouseManager.Instance.GetCursorOnObject() == this.gameObject)
        {
            islandImage.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }
        else
        {

            islandImage.transform.localScale = new Vector3(1,1,1);
        }
    }
}
