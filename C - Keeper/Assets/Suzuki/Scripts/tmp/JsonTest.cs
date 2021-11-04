using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class InputJson
//{
//    public string[] FamilyNameList;
//    public string[] FirstNameList;
//}


public class JsonTest : MonoBehaviour
{
    public string name;

    // Start is called before the first frame update
    void Start()
    {
        string inputString = Resources.Load<TextAsset>("jsontest").ToString();
        InputJson inputJson = JsonUtility.FromJson<InputJson>(inputString);

        int rand1 = Random.Range(0, inputJson.FirstNameList.Length);
        int rand2 = Random.Range(0, inputJson.FamilyNameList.Length);

        name = inputJson.FirstNameList[rand1] + " " + inputJson.FamilyNameList[rand2];

        Debug.Log(name);
        Debug.Log(inputJson.FirstNameList.Length);
        Debug.Log(inputJson.FamilyNameList.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
