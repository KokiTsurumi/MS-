using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUpUI : SingletonMonoBehaviour<RankUpUI>
{
    // Start is called before the first frame update

    [SerializeField]
    Text text;

    [SerializeField]
    GameObject recruitCanvasPrefab;

    [SerializeField]
    GameObject rankUICanvas;

    int rankUp = 1;

    //汚染度とランクアップのキャンバスがかぶらないようにするセマフォ
    //public bool pollutionRecruitCanvas = false;
    //public bool rankUpRecruitCanvas = false;
    public bool useCanvas = false;//排他制御

    void Start()
    {
        rankUICanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
     
    }


    public void Create()
    {
        text.text ="ランクが上がったよ。\nﾔｯﾀﾈ";
        WorldManager.Instance.IncreasePopularityRank();

        CharacterManager.Instance.CreateCandidateCharacter();
        rankUICanvas.SetActive(true);
    }

    public void OnClickClose()
    {
        //人材選択UI表示
        GameObject obj = Instantiate(recruitCanvasPrefab);
        obj.GetComponent<RecruitCanvas>();
        rankUICanvas.SetActive(false);
    }

    public void RankUpCheck()
    {
        //汚染度0％のときの人材選択中はランクアップさせない
        //if (pollutionRecruitCanvas)
        //    return;
        if (TutorialManager.Instance.tutorialState != TutorialManager.TutorialState.No)
        {
            rankUp = 2;
            return;
        }

        if (useCanvas) return;

        Name_Value.Instance.RankConfirm();

        if (rankUp < Name_Value.Instance.myRank)
        {
            Create();
            rankUp = Name_Value.Instance.myRank;
            useCanvas = true;
            Camera.main.GetComponent<CameraController>().SetAction(false);
        }

    }
}
