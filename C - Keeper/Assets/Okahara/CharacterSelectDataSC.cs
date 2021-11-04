using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectDataSC : MonoBehaviour
{
    // Start is called before the first frame update


    Button button;

    void Start()
    {
        button = this.GetComponent<Button>();

        button.onClick.AddListener(CharacterDecision);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharacterDecision()
    {
        //if (eventSystem.currentSelectedGameObject != null)
        //{
        //    GameObject obj = eventSystem.currentSelectedGameObject.gameObject;


        //}
        //else
        //    image.sprite = null;

    }
}
