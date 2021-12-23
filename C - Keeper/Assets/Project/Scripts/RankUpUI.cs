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

    //�����x�ƃ����N�A�b�v�̃L�����o�X�����Ԃ�Ȃ��悤�ɂ���Z�}�t�H
    //public bool pollutionRecruitCanvas = false;
    //public bool rankUpRecruitCanvas = false;
    public bool useCanvas = false;//�r������

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
        text.text ="�����N���オ������B\nԯ��";
        WorldManager.Instance.IncreasePopularityRank();

        CharacterManager.Instance.CreateCandidateCharacter();
        rankUICanvas.SetActive(true);
    }

    public void OnClickClose()
    {
        //�l�ޑI��UI�\��
        GameObject obj = Instantiate(recruitCanvasPrefab);
        obj.GetComponent<RecruitCanvas>();
        rankUICanvas.SetActive(false);
    }

    public void RankUpCheck()
    {
        //�����x0���̂Ƃ��̐l�ޑI�𒆂̓����N�A�b�v�����Ȃ�
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
