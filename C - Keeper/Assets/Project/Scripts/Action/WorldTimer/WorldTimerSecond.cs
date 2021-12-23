using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldTimerSecond : MonoBehaviour
{
    [SerializeField] Image onesPlaceTime;
    [SerializeField] Image tensPlaceTime;

    [SerializeField]
    Sprite[] numbers = new Sprite[10];

    private int time = 0;


    private void FixedUpdate()
    {
        int t = (int)WorldManager.Instance.currentTime;

        if (time == t) return;
        time = t;

        int onesTime = time % 10;
        int tensTime = time / 10;

        onesPlaceTime.sprite = numbers[onesTime];
        tensPlaceTime.sprite = numbers[tensTime];

        //è\ÇÃà Ç™0ÇÃèÍçáîÒï\é¶
        if(tensTime == 0)
        {
            tensPlaceTime.enabled = false;
        }
        else
        {
            tensPlaceTime.enabled = true;
        }
    }

}
