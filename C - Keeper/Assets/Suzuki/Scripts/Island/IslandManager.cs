using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : SingletonMonoBehaviour<IslandManager>
{
    public GameObject currentIsland;



    // ���݂̓���Ԃ��֐�
    public GameObject GetCurrentIsland()
    {
        return currentIsland;
    }

    // ���݂̓����Z�b�g����֐�
    public void SetCurrentIsland(GameObject island)
    {
        currentIsland = island;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
