using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonsize : MonoBehaviour
{
    public Button buttonObj;
    private RectTransform rectTF;
    public AnimationCurve animaCurve;

    void Start()
    {
        rectTF = buttonObj.GetComponent<RectTransform>();
    }

    public void OnPointerEnterEvent()
    {
        StartCoroutine(PointerBig());
    }
    private IEnumerator PointerBig()
    {
        for (float i = 0; i < 1; i = i + 0.01f)
        {
            float x = animaCurve.Evaluate(i);
            rectTF.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100 * x + 80 - 80 * x);
            rectTF.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 110 * x + 100 - 100 * x);
            yield return null;
        }
    }

    public void OnPointerExitEvent()
    {
        StartCoroutine(PointerSmall());
    }
    private IEnumerator PointerSmall()
    {
        for (float i = 0; i < 1; i = i + 0.01f)
        {
            float x = animaCurve.Evaluate(i);
            rectTF.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 80 * x + 100 - 100 * x);
            rectTF.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100 * x + 110 - 110 * x);
            yield return null;
        }
    }
}