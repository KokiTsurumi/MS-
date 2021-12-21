using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCursor : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Image cursor; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition(Vector2 pos)
    {
        cursor.rectTransform.position = new Vector3(pos.x, pos.y, 0);
    }
}
