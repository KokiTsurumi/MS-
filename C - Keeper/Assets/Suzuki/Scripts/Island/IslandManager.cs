using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : SingletonMonoBehaviour<IslandManager>
{
    public GameObject currentIsland;



    // 現在の島を返す関数
    public GameObject GetCurrentIsland()
    {
        return currentIsland;
    }

    // 現在の島をセットする関数
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
