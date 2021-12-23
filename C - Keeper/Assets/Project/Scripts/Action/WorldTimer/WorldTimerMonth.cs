using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldTimerMonth : MonoBehaviour
{
    [SerializeField] Image onesPlaceMonth;
    [SerializeField] Image tensPlaceMonth;

    [SerializeField]
    Sprite[] numbers = new Sprite[10];

    private int month = 0;


    private void FixedUpdate()
    {
        int m = (int)WorldManager.Instance.month;

        if (month == m) return;
        month = m;

        int onesTime = month % 10;
        int tensTime = month / 10;

        onesPlaceMonth.sprite = numbers[onesTime];
        tensPlaceMonth.sprite = numbers[tensTime];

        //è\ÇÃà Ç™0ÇÃèÍçáîÒï\é¶
        if (tensTime == 0)
        {
            tensPlaceMonth.enabled = false;
        }
        else
        {
            tensPlaceMonth.enabled = true;
        }
    }

}
