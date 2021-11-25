using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitCanvas : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject recruitCharacterPrefab;

    [SerializeField]
    GameObject charactersParent;

    [SerializeField]
    GameObject displayCharacter;

    [SerializeField]
    List<GameObject> recruitList;
    int dispListNumber = 0;

    void Start()
    {
        //test
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
            GetRecruitCharacterData();
    }

    void GetRecruitCharacterData()
    {
        //Character生成



        //Character取得

        //CharacterManager.Instance.characterList.Countは仮状態
        //採用候補と従業員リストは分ける
        for (int i = 0;i < CharacterManager.Instance.characterList.Count;i++)
        {
            GameObject original = CharacterManager.Instance.characterList[i];

            //Characterデータセット
            //for文
            GameObject recruitChara = (GameObject)Instantiate(recruitCharacterPrefab);

            recruitChara.GetComponent<RecruitCharacterData>().SetCharacterData(original);
            recruitChara.transform.parent = charactersParent.transform;
            recruitList.Add(recruitChara);



        }

            //一人目を表示させる
            displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber]);
    }

    public void OnClickLeftButton()
    {
        if (dispListNumber != 0)
            dispListNumber -= 1;
        else
            dispListNumber = recruitList.Count;//最大値

        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber]);
    }

    public void OnClickRightButton()
    {
        if (dispListNumber != recruitList.Count)
            dispListNumber += 1;
        else
            dispListNumber = 0;

        displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber]);
    }
}
