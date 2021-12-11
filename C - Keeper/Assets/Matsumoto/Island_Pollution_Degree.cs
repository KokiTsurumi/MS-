using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Island_Pollution_Degree : MonoBehaviour
{
    Slider islandPollutionSlider;
    float maxDegree = 100.0f;   //浄化度100%


    // Start is called before the first frame update
    void Start()
    {
        //浄化度の最大値の設定
        islandPollutionSlider = GetComponent<Slider>();
        islandPollutionSlider.maxValue = maxDegree;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject island = IslandManager.Instance.GetCurrentIsland();
        int level = island.GetComponent<IslandBase>().GetPollutionLevel();
        islandPollutionSlider.value = level;
        //Debug.Log("島の現在値：" + islandPollutionSlider.value);
    }
}
