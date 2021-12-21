using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitNavigatorCanvas : MonoBehaviour
{
    [SerializeField] GameObject text;

    [SerializeField] GameObject mainCanvas;

    bool start = false;

    //// Start is called before the first frame update
    void Start()
    {
        text.GetComponent<Text>().text = "この島の住人が、団体に是非参加したいと言ってきています！";
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void RecruitEnd()
    {
        //text.GetComponent<Text>().text = "これでまた一緒に働ける人が増えましたね！！引き続き綺麗にしていきましょう！";
        text.GetComponent<TextFader>().enabled = true;
        this.gameObject.SetActive(true);
        start = true;
    }

    public void OnClickNavigator()
    {
        if(text.GetComponent<TextFader>().enabled == true)
        {
            text.GetComponent<TextFader>().enabled = false;
            return;
        }
        

        this.gameObject.SetActive(false);

        if (start == false)
        {
            start = true;
            text.GetComponent<Text>().text = "これでまた一緒に働ける人が増えましたね！！引き続き綺麗にしていきましょう！";

            mainCanvas.SetActive(true);   
        }
        else
        {
            if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Recruit)
            {
                TutorialManager.Instance.NextStep();
            }
            else if (TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.RankUpRecruit)
            {
                TutorialManager.Instance.NextStep();
            }

            GameObject recruitCanvas = transform.root.gameObject;

            Name_Value.Instance.PlusPlacementCountt();
            RankUpUI.Instance.useCanvas = false;

            Destroy(recruitCanvas);
        }
   
    }
}
