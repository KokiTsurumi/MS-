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
       
        if(rankUp < Name_Value.Instance.myRank)
        {
            Create();
            rankUp = Name_Value.Instance.myRank;
        }

    }
}
