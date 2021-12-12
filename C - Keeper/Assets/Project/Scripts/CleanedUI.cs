using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanedUI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    Text text;

    [SerializeField]
    GameObject recruitCanvasPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void Create(GameObject island)
    {
        string name = island.GetComponent<IslandBase>().name;
        text.text = name + "はとってもきれいになったよ。\nﾔｯﾀﾈ";

        
    }

    public void OnClickClose()
    {
        if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Cleanning)
        {
            TutorialManager.Instance.NextStep();
            Destroy(this.gameObject);
            return;
        }

   
        //人材選択UI表示
        CharacterManager.Instance.CreateCandidateCharacter();
        GameObject obj = Instantiate(recruitCanvasPrefab);
        obj.GetComponent<RecruitCanvas>();
        Destroy(this.gameObject);
    }
}
