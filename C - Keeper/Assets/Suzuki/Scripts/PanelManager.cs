using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PanelManager : MonoBehaviour
{
    public GameObject CharacterManager;
    [SerializeField, Range(1, 5)]
    private int rank;

    private const int PANEL_MAX = 5;
    private GameObject[] Panels = new GameObject[PANEL_MAX];
    private GameObject[] Selected = new GameObject[2];

    private int panelIndex = 0;
    private int selectIndex = 0;

    public void Next()
    {
        panelIndex++;

        if (panelIndex >= PANEL_MAX)
            panelIndex = 0;
    }

    public void Previous()
    {
        panelIndex--;

        if (panelIndex < 0)
            panelIndex = PANEL_MAX - 1;
    }

    public void Select()
    {
        Selected[selectIndex] = Panels[panelIndex];
        selectIndex++;

        if (Selected[0] == Selected[1] && selectIndex >= 1)
        {
            selectIndex--;
            Selected[1] = null;
        }

        if(Selected[1] != null)
        {
            CharacterManager.GetComponent<CharacterManager>().character1 = Selected[0];
            CharacterManager.GetComponent<CharacterManager>().character2 = Selected[1];

            //Debug.Log(Selected[0]);
            //Debug.Log(Selected[1]);

            // éüÇÃÉVÅ[Éì
            SceneManager.LoadScene("PrototypeScene");
        }
    }

    public int GetIndex()
    {
        return panelIndex + 1;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        CharacterManager = GameObject.Find("CharacterManager");

        for(int i = 0; i < PANEL_MAX; i++)
        {
            Panels[i] = Instantiate((GameObject)Resources.Load("Panel"));
            Panels[i].GetComponent<CharacterPanel>().SetRank(rank);
        }

        for (int i = 0; i < 2; i++)
        {
            Selected[i] = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < PANEL_MAX; i++)
        {
            Panels[i].SetActive(false);
        }

        Panels[panelIndex].SetActive(true);
    }
}
