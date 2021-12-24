using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigCanvas : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Slider slider;

    [SerializeField] AudioSource audioSource;

    float value;

    void Start()
    {
        slider.maxValue = 1;
        slider.minValue = 0;
        slider.value = 0.2f;

        value = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if(value != slider.value)
        {
            value = slider.value;

            audioSource.volume = value;

        }
    }
}
