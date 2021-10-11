using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterPanel : MonoBehaviour
{
    public int research, work;  // キャラクターのパラメータ
    public Text researchText, workText;
    public string rank;

    private int index;
    public Image image;

    public void SetRank(int Rank)
    {
        if(Rank <= 1)
        {
            research = Random.Range(0, 2);
            work = Random.Range(0, 2);

            researchText.text = "研究　" + RankTransfer(research);
            workText.text = "作業　" + RankTransfer(work);
        }
        else if(Rank == 2)
        {
            research = Random.Range(0, 3);
            work = Random.Range(0, 3);

            researchText.text = "研究　" + RankTransfer(research);
            workText.text = "作業　" + RankTransfer(work);
        }
        else if(Rank == 3)
        {
            research = Random.Range(1, 4);
            work = Random.Range(1, 4);

            researchText.text = "研究　" + RankTransfer(research);
            workText.text = "作業　" + RankTransfer(work);
        }
        else if (Rank == 4)
        {
            research = Random.Range(2, 5);
            work = Random.Range(2, 5);

            researchText.text = "研究　" + RankTransfer(research);
            workText.text = "作業　" + RankTransfer(work);
        }
        else
        {
            research = Random.Range(3, 6);
            work = Random.Range(3, 6);

            researchText.text = "研究　" + RankTransfer(research);
            workText.text = "作業　" + RankTransfer(work);
        }
    }

    public string RankTransfer(int Param)
    {
        string tmp;

        if (Param <= 0)
            tmp = "E";
        else if (Param == 1)
            tmp = "D";
        else if (Param == 2)
            tmp = "C";
        else if (Param == 3)
            tmp = "B";
        else if (Param == 4)
            tmp = "A";
        else
            tmp = "S";

        return tmp;
    }

    // Start is called before the first frame update
    void Start()
    {
        //research = Random.Range(0, 7);
        //work = Random.Range(0, 7);

        //researchText.text = "研究　" + research.ToString();
        //workText.text = "作業　" + work.ToString();

        index = Random.Range(1, 6);
        image = transform.Find("Character/Image").GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>($"character{index}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
