using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : SingletonMonoBehaviour<IslandManager>
{
    public GameObject currentIsland;



    // Œ»İ‚Ì“‡‚ğ•Ô‚·ŠÖ”
    public GameObject GetCurrentIsland()
    {
        return currentIsland;
    }

    // Œ»İ‚Ì“‡‚ğƒZƒbƒg‚·‚éŠÖ”
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
