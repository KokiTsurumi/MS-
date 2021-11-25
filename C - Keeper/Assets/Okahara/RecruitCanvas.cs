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
        //Character����



        //Character�擾

        //CharacterManager.Instance.characterList.Count�͉����
        //�̗p���Ə]�ƈ����X�g�͕�����
        for (int i = 0;i < CharacterManager.Instance.characterList.Count;i++)
        {
            GameObject original = CharacterManager.Instance.characterList[i];

            //Character�f�[�^�Z�b�g
            //for��
            GameObject recruitChara = (GameObject)Instantiate(recruitCharacterPrefab);

            recruitChara.GetComponent<RecruitCharacterData>().SetCharacterData(original);
            recruitChara.transform.parent = charactersParent.transform;
            recruitList.Add(recruitChara);



        }

            //��l�ڂ�\��������
            displayCharacter.GetComponent<RecruitCharacterData>().SetCharacterData(recruitList[dispListNumber]);
    }

    public void OnClickLeftButton()
    {
        if (dispListNumber != 0)
            dispListNumber -= 1;
        else
            dispListNumber = recruitList.Count;//�ő�l

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
