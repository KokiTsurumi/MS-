using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUpComment : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image serRankFixedComment;
    [SerializeField] Image setRankComment;

    [SerializeField] Sprite[] rankUpComments = new Sprite[3];


    int myRank = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myRank == Name_Value.Instance.myRank) return;

        myRank = Name_Value.Instance.myRank;


        switch(myRank)
        {
            case 2:
                setRankComment.sprite = rankUpComments[0];
                serRankFixedComment.enabled = true;
                setRankComment.enabled = true;
                break;

            case 3:
                setRankComment.sprite = rankUpComments[1];
                serRankFixedComment.enabled = true;
                setRankComment.enabled = true;
                break;
            case 4:
                setRankComment.sprite = rankUpComments[2];
                serRankFixedComment.enabled = true;
                setRankComment.enabled = true;
                break;
            default:
                serRankFixedComment.enabled = false;
                setRankComment.enabled = false;

                break;
        }

    }
}
