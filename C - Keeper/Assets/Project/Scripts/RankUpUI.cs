using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUpUI : SingletonMonoBehaviour<RankUpUI>
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject recruitCanvasPrefab;

    [SerializeField]
    GameObject rankUICanvas;

    [SerializeField] AudioClip rankUpSound;

    AudioSource audioSource;

    int rankUp = 1;

    bool go = false;

    //�����x�ƃ����N�A�b�v�̃L�����o�X�����Ԃ�Ȃ��悤�ɂ���Z�}�t�H
    //public bool pollutionRecruitCanvas = false;
    //public bool rankUpRecruitCanvas = false;
    public bool useCanvas = false;//�r������

    void Start()
    {
        rankUICanvas.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }


    public void Create()
    {
        WorldManager.Instance.IncreasePopularityRank();

        CharacterManager.Instance.CreateCandidateCharacter();
        rankUICanvas.SetActive(true);

        audioSource.PlayOneShot(rankUpSound);

        StartCoroutine(DisplayCoroutine());
    }

    public void OnClickClose()
    {
        if (!go) return;

        //�l�ޑI��UI�\��
        GameObject obj = Instantiate(recruitCanvasPrefab);
        obj.GetComponent<RecruitCanvas>();
        rankUICanvas.SetActive(false);

        go = false;
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
            Camera.main.GetComponent<CameraController>().SetTransState(CameraController.TransState.CENTER);
        }

    }

    IEnumerator DisplayCoroutine()
    {
        yield return new WaitForSeconds(2.0f);

        go = true;
    }
}
