using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelManager : MonoBehaviour
{
    private const int PANEL_MAX = 5;
    private GameObject[] Panels = new GameObject[PANEL_MAX];

    private int index = 0;
    private Image image;

    public void Next()
    {
        index++;

        if (index >= PANEL_MAX)
            index = 0;
    }

    public void Previous()
    {
        index--;

        if (index < 0)
            index = PANEL_MAX - 1;
    }

    public int GetIndex()
    {
        return index + 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < PANEL_MAX; i++)
        {
            Panels[i] = Instantiate((GameObject)Resources.Load("Panel"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < PANEL_MAX; i++)
        {
            Panels[i].SetActive(false);
        }

        Panels[index].SetActive(true);
    }
}
