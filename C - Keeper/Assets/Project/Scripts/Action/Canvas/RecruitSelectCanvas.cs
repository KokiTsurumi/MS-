using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 　人材選択　キャラクターリスト　UI　クラス
/// </summary>
public class RecruitSelectCanvas : MonoBehaviour
{
    [SerializeField] GameObject recruitCharacterPrefab;

    [SerializeField] GameObject charactersParent;

    [SerializeField] GameObject displayCharacter;

    [SerializeField] GameObject mainCanvas;

    [SerializeField] Button button;


    int dispListNumber = 0;

    List<GameObject> recruitList = new List<GameObject>();

    public GameObject GetDisplayCharacter => recruitList[dispListNumber];



    void Start()
    {


        for (int i = 0; i < CharacterManager.Instance.candidateList.Count; i++)
        {
            GameObject recruitChara = (GameObject)Instantiate(recruitCharacterPrefab);
            recruitChara.GetComponent<RecruitCharacterData>().Create(CharacterManager.Instance.candidateList[i]);

            recruitChara.transform.SetParent(charactersParent.transform, false);

            recruitList.Add(recruitChara);
        }

        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber].GetComponent<RecruitCharacterData>());

        this.gameObject.SetActive(false);
    }

    public void OnClickLeftButton()
    {

        if (dispListNumber != 0)
            dispListNumber -= 1;
        else
            dispListNumber = recruitList.Count - 1;//最大値

        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber].GetComponent<RecruitCharacterData>());
        button.enabled= ! recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected;
    }

    public void OnClickRightButton()
    {
        if (dispListNumber != recruitList.Count - 1)
            dispListNumber += 1;
        else
            dispListNumber = 0;

        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber].GetComponent<RecruitCharacterData>());
        button.enabled = ! recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected;

    }

    public void OnClickSelectButton()
    {

        recruitList[dispListNumber].GetComponent<RecruitCharacterData>().selected = true;

        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber].GetComponent<RecruitCharacterData>());

        button.enabled = false;

        this.gameObject.SetActive(false);
    }

    public void OnClickBackButton()
    {
        this.gameObject.SetActive(false);
    }
}
