using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpCharacterManager : MonoBehaviour
{
    public GameObject character1, character2;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(character1);
        Debug.Log(character2);

    }
}
